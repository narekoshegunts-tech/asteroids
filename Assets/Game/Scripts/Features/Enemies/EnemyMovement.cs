using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using Game.Scripts.Features.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies
{
    public abstract class EnemyMovement: MonoBehaviour, ITeleportable, ICollisionable
    {
        protected CustomPhysicsFacade2D _customPhysicsFacade2D;

        protected float _speed;
        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacade2DFactory)
        {
            _customPhysicsFacade2D = customPhysicsFacade2DFactory.Create(transform);
        }

        protected void FixedUpdate()
        {
            _customPhysicsFacade2D.FixedUpdate();
        }
        
        protected void MoveTo(Vector3 position)
        {
            _customPhysicsFacade2D.MoveTo(position, _speed);
        }
        
        public CustomPhysicsFacade2D GetCustomPhysicsFacade2D()
        {
            return _customPhysicsFacade2D;
        }
    }
}