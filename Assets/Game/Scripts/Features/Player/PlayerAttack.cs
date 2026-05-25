using Game.Scripts.Common.CustomInput;
using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerAttack: MonoBehaviour
    {
        private PlayerModel _playerModel;
        
        [Inject] private CustomInputSystem _customInputSystem;
        
        [SerializeField] private Transform _attackStartTransform;

        // Нужно чтобы получить доступ к вращению игрока, для передачи вращения снаряду. Хз как по другому
        private PlayerMovement _playerMovement;
        
        [Inject] private BulletAttackService _bulletAttackService;
        [Inject] private LaserAttackService _laserAttackService;
        
        [Inject] private PlayerStateService _playerStateService;

        [Inject]
        private void Construct(PlayerModel playerModel, PlayerMovement playerMovement)
        {
            _playerModel = playerModel;
            _playerMovement = playerMovement;
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