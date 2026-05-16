using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using Game.Scripts.Features.Enemies.Asteroids.Data;
using Game.Scripts.Features.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies.Asteroids
{
    public class AsteroidMovement: MonoBehaviour, ITeleportable, ICollisionable
    {
        private CustomPhysicsFacade2D _customPhysicsFacade2D;
        
        private float _speed;

        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacade2DFactory)
        {
            _customPhysicsFacade2D = customPhysicsFacade2DFactory.Create(transform);
        }

        public void Init(Vector3 startPosition, AsteroidData data, Vector3 targetPosition)
        {
            _customPhysicsFacade2D.ApplyPosition(startPosition);
            _customPhysicsFacade2D.Mass = data.Mass;
            
            _speed = Random.Range(data.MinSpeed, data.MaxSpeed);
            
            MoveTo(targetPosition);
        }

        public void Init(Vector3 startPosition, AsteroidData data)
        {
            _customPhysicsFacade2D.ApplyPosition(startPosition);
            
            _speed = Random.Range(data.MinSpeed, data.MaxSpeed);
            
            _customPhysicsFacade2D.SetRandomDirectionToMove(_speed);
        }

        private void FixedUpdate()
        {
            _customPhysicsFacade2D.FixedUpdate();
        }

        public void MoveTo(Vector3 position)
        {
            _customPhysicsFacade2D.MoveTo(position, _speed);
        }

        public CustomPhysicsFacade2D GetCustomPhysicsFacade2D()
        {
            return _customPhysicsFacade2D;
        }
    }
}