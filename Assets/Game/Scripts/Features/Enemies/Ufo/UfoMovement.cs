using Game.Scripts.Features.Enemies.Ufo.Data;
using Game.Scripts.Features.Player;
using Game.Scripts.Features.Player.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies.Ufo
{
    public class UfoMovement: EnemyMovement
    {
        private Transform _targetTransform;

        [Inject]
        private void Construct(IPlayerTransform player)
        {
            _targetTransform = player.Transform;
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