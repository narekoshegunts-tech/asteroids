using System.Collections;
using Game.Scripts.Common.CustomInput;
using Game.Scripts.Common.ObjectPool;
using Game.Scripts.Features.Player.Projectiles;
using Game.Scripts.Features.Player.Projectiles.Bullets;
using Game.Scripts.Features.Player.Projectiles.Data;
using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerAttack: MonoBehaviour
    {
        [Inject] private CustomInputSystem _customInputSystem;
        
        [SerializeField] private Transform _attackStartPosition;

        // Нужно чтобы получить доступ к вращению игрока, для передачи вращения снаряду. Хз как по другому
        private PlayerMovement _playerMovement;
        
        [Inject] private BulletAttackService _bulletAttackService;

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
            _bulletAttackService.Attack(_attackStartPosition.position, _playerMovement.Direction);
        }
        
    }
}