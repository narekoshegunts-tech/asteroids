using Game.Scripts.Common.CustomInput;
using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerAttack: MonoBehaviour
    {
        private PlayerModel _playerModel;
        
        private CustomInputSystem _customInputSystem;
        
        [SerializeField] private Transform _attackStartTransform;

        // Нужно чтобы получить доступ к вращению игрока, для передачи вращения снаряду. Хз как по другому
        private PlayerMovement _playerMovement;
        
        private BulletAttackService _bulletAttackService;
        private LaserAttackService _laserAttackService;
        
        private PlayerStateService _playerStateService;

        [Inject]
        private void Construct(PlayerModel playerModel, PlayerMovement playerMovement,
            CustomInputSystem customInputSystem, BulletAttackService bulletAttackService, LaserAttackService laserAttackService,
            PlayerStateService playerStateService)
        {
            _playerModel = playerModel;
            _playerMovement = playerMovement;
            _customInputSystem = customInputSystem;
            _bulletAttackService = bulletAttackService;
            _laserAttackService = laserAttackService;
            _playerStateService = playerStateService;
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
            
            _bulletAttackService.Attack(_attackStartTransform.position, _playerMovement.Direction);
        }

        private void LaserAttack()
        {
            if (!_playerStateService.CanAttack)
                return;
            
            if (_playerModel.TryLaserAttack())
                _laserAttackService.Attack(_attackStartTransform.position, _playerMovement.Direction);
        }
        
    }
}