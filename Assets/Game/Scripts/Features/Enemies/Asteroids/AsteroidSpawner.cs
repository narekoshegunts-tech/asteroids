using System.Collections.Generic;
using Game.Scripts.Common;
using Game.Scripts.Common.ObjectPool;
using Game.Scripts.Features.Enemies.Asteroids.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies.Asteroids
{
    public class AsteroidSpawner: MonoBehaviour
    {
        [Inject] private CameraService _cameraService;
        
        private Dictionary<AsteroidType, AsteroidData> _asteroidsData;
        private ObjectPool<Asteroid> _asteroidsPool;
        
        [SerializeField] private Asteroid _asteroidPrefab;
        
        

        [Inject]
        private void Construct(ObjectPoolFactory objectPoolFactory, AsteroidDataService asteroidDataService)
        {
            _asteroidsData = asteroidDataService.GetData();
            
            GameObject asteroidsContainer = new GameObject("AsteroidsPool");
            _asteroidsPool = objectPoolFactory.Create(_asteroidPrefab, asteroidsContainer, asteroidDataService.PoolSize);
        }

        private void Start()
        {
            for (int i = 0; i < 5; i++) 
                SpawnLarge();
        }

        private void SpawnLarge()
        {
            if (_asteroidsPool.TryGet(out Asteroid asteroid))
            {
                Vector2 spawnPosition = _cameraService.GetOffscreenPosition();
                Vector2 targetPosition = _cameraService.GetScreenRandomPosition();
                asteroid.Initialize(spawnPosition, targetPosition, _asteroidsData[AsteroidType.Large]);
            }
        }
    }
}