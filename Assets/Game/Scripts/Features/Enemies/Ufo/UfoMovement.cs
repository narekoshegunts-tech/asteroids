using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using Game.Scripts.Features.Enemies.Asteroids.Data;
using Game.Scripts.Features.Enemies.UFO.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies.UFO
{
    public class UfoMovement: MonoBehaviour
    {
        private CustomPhysicsFacade2D _customPhysicsFacade2D;

        private Transform _targetTransform;
        private float _speed;

        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacade2DFactory, Player.Player player)
        {
            _targetTransform = player.transform;
            
            _customPhysicsFacade2D = customPhysicsFacade2DFactory.Create(transform);
        }

        public void Init(Vector3 startPosition, UfoData data)
        {
            _customPhysicsFacade2D.ApplyPosition(startPosition);
            _customPhysicsFacade2D.Mass = data.Mass;
            
            _speed = Random.Range(data.MinSpeed, data.MaxSpeed);
        }

        private void FixedUpdate()
        {
            _customPhysicsFacade2D.FixedUpdate();
            MoveTo(_targetTransform.position);
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