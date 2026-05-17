using System.Collections;
using Game.Scripts.Common.CustomInput;
using Game.Scripts.Common.ObjectPool;
using Game.Scripts.Features.Player.Projectiles;
using Game.Scripts.Features.Player.Projectiles.Bullets;
using Game.Scripts.Features.Player.Projectiles.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerAttack: MonoBehaviour
    {
        [Inject] private CustomInputSystem _customInputSystem;
        
        private ObjectPool<Bullet> _bulletPool;
        
        private GameObject _bulletPoolContainer;
        [SerializeField] private Bullet _bulletPrefab;

        [SerializeField] private Transform _bulletAttackStartPosition;

        // Нужно чтобы получить доступ к вращению игрока, для передачи вращения пуле. Хз как по другому
        private PlayerMovement _playerMovement;

        [Inject]
        private void Construct(ObjectPoolFactory objectPoolFactory, CustomInputSystem customInputSystem,
            ProjectilesDataService projectilesDataService)
        {
            // хз можно ли использовать new для создания контейнеров. Думаю нет смысла инжектить GameObject 
            _bulletPoolContainer = new GameObject("BulletPool");
            
            int bulletPoolSize = projectilesDataService.BulletPoolSize;
            
            _bulletPool = objectPoolFactory.Create<Bullet>(_bulletPrefab, _bulletPoolContainer, bulletPoolSize);
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
            if (_bulletPool.TryGet(out var bullet))
            {
                bullet.Init(_bulletAttackStartPosition.position, _playerMovement.Direction);
                bullet.gameObject.SetActive(true);
                bullet.OnDestroy += ReturnBulletToPool;
            }

        }

        private void ReturnBulletToPool(Projectile projectile)
        {
            _bulletPool.Return(projectile as Bullet);
            projectile.OnDestroy -= ReturnBulletToPool;
        }
        
    }
}