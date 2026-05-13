using System.Collections.Generic;
using Game.Scripts.Common.ObjectPool;
using Game.Scripts.Features.Enemies.Asteroids.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies.Asteroids
{
    public class AsteroidSpawner: MonoBehaviour
    {
        private Dictionary<AsteroidType, AsteroidData> _asteroidsData;
        ObjectPool<Asteroid> _asteroidsPool;
        
        [SerializeField] Asteroid _asteroidPrefab;
        [SerializeField] private int _poolSize;

        [Inject]
        private void Construct(ObjectPoolFactory objectPoolFactory, AsteroidDataService asteroidDataService)
        {
            _asteroidsData = asteroidDataService.GetData();
            
            GameObject asteroidsContainer = new GameObject("AsteroidsPool");
            _asteroidsPool = objectPoolFactory.Create(_asteroidPrefab, asteroidsContainer, asteroidDataService.PoolSize);
        }

        private void Start()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                SpawnLarge();
            }
        }

        private void SpawnLarge()
        {
            if (_asteroidsPool.TryGet(out Asteroid asteroid))
            {
                asteroid.Initialize(new Vector3(Random.Range(0, 100), Random.Range(0, 100), 0), _asteroidsData[AsteroidType.Large]);
            }
        }
    }
}