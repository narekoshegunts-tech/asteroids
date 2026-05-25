using Game.Scripts.Features.Enemies.Asteroids.Data;
using UnityEngine;

namespace Game.Scripts.Features.Enemies.Asteroids
{
    public class AsteroidMovement: EnemyMovement
    {
        public void Init(Vector3 startPosition, AsteroidData data, Vector3 targetPosition)
        {
            _customPhysicsFacade2D.ApplyPosition(startPosition);
            _customPhysicsFacade2D.Mass = data.Mass;
            
            _speed = Random.Range(data.MinSpeed, data.MaxSpeed);
            
            MoveTo(targetPosition);
        }
    }
}