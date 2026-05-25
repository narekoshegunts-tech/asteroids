using Game.Scripts.Features.Interfaces;
using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerDamageReceiver: MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        
        private PlayerModel _playerModel;
        
        private PlayerStateService _playerStateService;


        [Inject]
        private void Construct(PlayerModel playerModel, PlayerMovement playerMovement,
            PlayerStateService playerStateService)
        {
            _playerModel = playerModel;
            _playerMovement = playerMovement;
            _playerStateService = playerStateService;
        }
        
        private void GetDamage()
        {
            _playerModel.GetDamage();
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