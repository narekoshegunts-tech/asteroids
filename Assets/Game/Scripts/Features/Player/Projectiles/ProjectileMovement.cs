using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using Game.Scripts.Features.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player.Projectiles
{
    public abstract class ProjectileMovement: MonoBehaviour, ITeleportable
    {
        protected CustomPhysicsFacade2D _customPhysicsFacade2D;
        
        protected float _speed;
        
        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacade2DFactory)
        {
            _customPhysicsFacade2D = customPhysicsFacade2DFactory.Create(transform);
        }
        
        public void Init(Vector3 startPosition, Vector2 direction)
        {
            _customPhysicsFacade2D.ApplyPosition(startPosition);
            _customPhysicsFacade2D.ApplyRotation(direction);
                        
            _customPhysicsFacade2D.ApplyVelocity(_speed);
        }
        
        protected void FixedUpdate()
        {
            _customPhysicsFacade2D.FixedUpdate();
        }
        
        public CustomPhysicsFacade2D GetCustomPhysicsFacade2D()
        {
            return _customPhysicsFacade2D;
        }
    }
}