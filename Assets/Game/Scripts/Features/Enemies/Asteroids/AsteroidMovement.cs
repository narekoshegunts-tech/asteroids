using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using Game.Scripts.Features.Enemies.Asteroids.Data;
using Game.Scripts.Features.Interfaces;
using UnityEngine;
using Zenject;

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