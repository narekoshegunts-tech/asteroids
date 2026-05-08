using System.Collections;
using Game.Scripts.Common.CustomInput;
using Game.Scripts.Common.ObjectPool;
using Game.Scripts.Features.Player.Attack;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    // Класс еще и хранит в себе обжект пулы. Надеюсь это не нарушает SRP. Он же по сути занимается только атакой
    public class PlayerAttack: MonoBehaviour
    {
        [Inject] private CustomInputSystem _customInputSystem;
        
        private ObjectPool<Bullet> _bulletPool;
        
        private GameObject _bulletPoolContainer;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private int _bulletPoolSize;

        [SerializeField] private Transform _bulletAttackStartPosition;

        // Нужно чтобы получить доступ к вращению игрока, для передачи вращения пуле. Хз как по другому
        private PlayerMovement _playerMovement;

        [Inject]
        private void Construct(ObjectPoolFactory objectPoolFactory, CustomInputSystem customInputSystem)
        {
            // хз можно ли использовать new для создания контейнеров. Думаю нет смысла инжектить GameObject 
            _bulletPoolContainer = new GameObject("BulletPool");
            
            _bulletPool = objectPoolFactory.Create<Bullet>(_bulletPrefab, _bulletPoolContainer, _bulletPoolSize);
        }

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void OnEnable()
        {
            _customInputSystem.OnBulletAttackKeyPressed += BulletAttack;
        }

        private void OnDisable()
        {
            _customInputSystem.OnBulletAttackKeyPressed -= BulletAttack;
        }


        private void BulletAttack()
        {
            StartCoroutine(BulletAttackCoroutine());
        }

        private IEnumerator BulletAttackCoroutine()
        {
            if (_bulletPool.TryGet(out var bullet))
            {
                bullet.Init(_bulletAttackStartPosition.position, _playerMovement.Direction);
                bullet.gameObject.SetActive(true);
                yield return new WaitForSeconds(3);
                
                _bulletPool.Return(bullet);
            }
        }
    }
}