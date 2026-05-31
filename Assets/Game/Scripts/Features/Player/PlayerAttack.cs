using Game.Scripts.Common.CustomInput;
using Game.Scripts.Features.Player.Interfaces;
using Game.Scripts.Features.Player.Model;
using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerAttack: MonoBehaviour
    {
        private PlayerLaserAttackModel _playerLaserAttackModel;
        
        private CustomInputSystem _customInputSystem;
        
        [SerializeField] private Transform _attackStartTransform;
        
        private IPlayerDirection _playerDirection;
        
        private BulletAttackService _bulletAttackService;
        private LaserAttackService _laserAttackService;
        
        private PlayerStateService _playerStateService;

        [Inject]
        private void Construct(PlayerLaserAttackModel playerLaserAttackModel, IPlayerDirection playerDirection,
            CustomInputSystem customInputSystem, BulletAttackService bulletAttackService, LaserAttackService laserAttackService,
            PlayerStateService playerStateService)
        {
            _playerLaserAttackModel = playerLaserAttackModel;
            _playerDirection = playerDirection;
            _customInputSystem = customInputSystem;
            _playerStateService = playerStateService;
            
            _bulletAttackService = bulletAttackService;
            _laserAttackService = laserAttackService;
            _bulletAttackService.Initialize();
            _laserAttackService.Initialize();
        }
        

        private void OnEnable()
        {
            _customInputSystem.OnBulletAttackKeyPressed += BulletAttack;
            _customInputSystem.OnLaserAttackKeyPressed += LaserAttack;
        }

        private void OnDisable()
        {
            _customInputSystem.OnBulletAttackKeyPressed -= BulletAttack;
            _customInputSystem.OnLaserAttackKeyPressed -= LaserAttack;
        }


        private void BulletAttack()
        {
            if (!_playerStateService.CanAttack)
                return;
            
            _bulletAttackService.Attack(_attackStartTransform.position, _playerDirection.Direction);
        }

        private void LaserAttack()
        {
            if (!_playerStateService.CanAttack)
                return;
            
            if (_playerLaserAttackModel.TryLaserAttack())
                _laserAttackService.Attack(_attackStartTransform.position, _playerDirection.Direction);
        }
        
    }
}