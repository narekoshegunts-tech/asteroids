using Game.Scripts.Features.Interfaces;
using UnityEngine;

namespace Game.Scripts.Features.Player
{
    public class Player: MonoBehaviour
    {
        
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }
        
        private void GetDamage()
        {
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