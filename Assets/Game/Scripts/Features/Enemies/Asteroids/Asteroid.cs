using System;
using Game.Scripts.Common;
using UnityEngine;
using Game.Scripts.Features.Enemies.Asteroids.Data;
using Random = UnityEngine.Random;

namespace Game.Scripts.Features.Enemies.Asteroids
{
    [RequireComponent(typeof(AsteroidMovement))]
    public class Asteroid: Enemy
    {
        public AsteroidType Type { get; private set; }
        
        private AsteroidMovement _asteroidMovement;


        private void Awake()
        {
            _asteroidMovement = GetComponent<AsteroidMovement>();
        }

        public void Initialize(Vector3 startPosition, Vector3 targetPosition, AsteroidData data)
        {
            Type = data.Type;

            var scale = Random.Range(data.MinScale, data.MaxScale);
            transform.localScale = scale * Vector3.one;
            
            _asteroidMovement.Init(startPosition, data, targetPosition);
        }
        
    }
}