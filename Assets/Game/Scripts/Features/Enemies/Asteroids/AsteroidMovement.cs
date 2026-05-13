using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using Game.Scripts.Features.Enemies.Asteroids.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies.Asteroids
{
    public class AsteroidMovement: MonoBehaviour
    {
        private CustomPhysicsFacade2D _customPhysicsFacade2D;
        
        private float _speed;

        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacade2DFactory)
        {
            _customPhysicsFacade2D = customPhysicsFacade2DFactory.Create(transform);
        }

        public void Init(Vector3 startPosition, Vector3 targetPosition, AsteroidData data)
        {
            _customPhysicsFacade2D.ApplyPosition(startPosition);
            
            _speed = Random.Range(data.MinSpeed, data.MaxSpeed);
            
            MoveTo(targetPosition);
        }

        private void Update()
        {
            _customPhysicsFacade2D.Update();
        }

        public void MoveTo(Vector3 position)
        {
            _customPhysicsFacade2D.MoveTo(position, _speed);
        }
    }
}