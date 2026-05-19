using Game.Scripts.Features.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerDamageReciever: MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        
        private PlayerModel _playerModel;


        [Inject]
        private void Construct(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }
        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }
        
        private void GetDamage()
        {
            _playerModel.GetDamage();
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ICollisionable>(out var other))
            {
                _playerMovement.GetCustomPhysicsFacade2D().Collision(other.GetCustomPhysicsFacade2D());
                GetDamage();
            }
        }
    }
}