using Game.Scripts.Features.Interfaces;
using Game.Scripts.Features.Player.Model;
using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerDamageReceiver: MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        
        private PlayerHealthModel _playerHealthModel;
        
        private PlayerStateService _playerStateService;


        [Inject]
        private void Construct(PlayerMovement playerMovement,
            PlayerStateService playerStateService, PlayerHealthModel playerHealthModel)
        {
            _playerMovement = playerMovement;
            _playerStateService = playerStateService;
            _playerHealthModel = playerHealthModel;
        }
        
        private void GetDamage()
        {
            _playerHealthModel.TakeDamage();
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_playerStateService.IsInvulnerable)
                return;
            
            if (collision.TryGetComponent<ICollisionable>(out var other))
            {
                _playerStateService.ApplyInvulnerability();
                _playerMovement.GetCustomPhysicsFacade2D().Collision(other.GetCustomPhysicsFacade2D());
                GetDamage();
            }
        }
    }
}