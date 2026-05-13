using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies.Asteroids
{
    public class AsteroidMovement: MonoBehaviour
    {
        private CustomPhysicsFacade2D _customPhysicsFacade2D;

        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        private float _speed;

        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacade2DFactory)
        {
            _customPhysicsFacade2D = customPhysicsFacade2DFactory.Create(transform);
        }

        public void Init(Vector3 startPosition,Vector3 targetPosition)
        {
            _customPhysicsFacade2D.ApplyPosition(startPosition);
            
            _speed = Random.Range(_minSpeed, _maxSpeed);
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