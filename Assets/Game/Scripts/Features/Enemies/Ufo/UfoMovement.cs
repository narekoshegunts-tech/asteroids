using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using Game.Scripts.Features.Enemies.UFO.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies.UFO
{
    public class UfoMovement: EnemyMovement
    {
        private Transform _targetTransform;

        [Inject]
        private void Construct(Player.Player player)
        {
            _targetTransform = player.transform;
        }

        public void Init(Vector3 startPosition, UfoData data)
        {
            _customPhysicsFacade2D.ApplyPosition(startPosition);
            _customPhysicsFacade2D.Mass = data.Mass;
            
            _speed = Random.Range(data.MinSpeed, data.MaxSpeed);
        }

        protected new void FixedUpdate()
        {
            base.FixedUpdate();
            MoveTo(_targetTransform.position);
        }
    }
}