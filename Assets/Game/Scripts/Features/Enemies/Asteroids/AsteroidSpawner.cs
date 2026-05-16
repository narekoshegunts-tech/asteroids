using System;
using System.Collections.Generic;
using System.Threading;
using Game.Scripts.Common.CameraServices;
using Game.Scripts.Common.ObjectPool;
using Game.Scripts.Features.Enemies.Asteroids.Data;
using UnityEngine;
using Zenject;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

namespace Game.Scripts.Features.Enemies.Asteroids
{
    public class AsteroidSpawner: MonoBehaviour
    {
        [Inject] private CameraUtils _cameraService;
        
        private Dictionary<AsteroidType, AsteroidData> _asteroidsData;
        private ObjectPool<Asteroid> _asteroidsPool;
        
        [SerializeField] private Asteroid _asteroidPrefab;

        [SerializeField] private float _largeAsteroidSpawnCooldown;

        private CancellationTokenSource _cts;

        [SerializeField] private int _maxLargeAsteroidsCount;
        private int _currentLargeAsteroidsCount;
        

        [Inject]
        private void Construct(ObjectPoolFactory objectPoolFactory, AsteroidDataService asteroidDataService)
        {
            _asteroidsData = asteroidDataService.GetData();
            
            GameObject asteroidsContainer = new GameObject("AsteroidsPool");
            _asteroidsPool = objectPoolFactory.Create(_asteroidPrefab, asteroidsContainer, asteroidDataService.PoolSize);
        }

        private void Awake()
        {
            _cts = new CancellationTokenSource();
        }

        private void Start()
        {
            SpawnAsteroids().Forget();
        }
        
        
        private async UniTaskVoid SpawnAsteroids()
        {
            while (_cts.IsCancellationRequested == false)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_largeAsteroidSpawnCooldown),
                    ignoreTimeScale: false,
                    cancellationToken: _cts.Token);
                
                if (_currentLargeAsteroidsCount < _maxLargeAsteroidsCount)
                {
                    SpawnLarge();    
                }
            }
        }

        private void SpawnLarge()
        {
            if (_asteroidsPool.TryGet(out Asteroid asteroid))
            {
                Vector2 spawnPosition = _cameraService.GetOffscreenPosition();
                Vector2 targetPosition = _cameraService.GetScreenRandomPosition();
                asteroid.Initialize(spawnPosition, targetPosition, _asteroidsData[AsteroidType.Large]);
                asteroid.OnDestroy += OnLargeAsteroidDestroyed;
                
                _currentLargeAsteroidsCount++;
            }
        }

        private void SpawnSmall(Vector2 spawnPosition, float offset = 1)
        {
            if (_asteroidsPool.TryGet(out Asteroid asteroid))
            {
                Vector2 vectorOffset = new Vector2(Random.Range(-offset, offset), Random.Range(-offset, offset));
                asteroid.Initialize(spawnPosition + vectorOffset, _asteroidsData[AsteroidType.Small]);

                asteroid.OnDestroy += OnSmallAsteroidDestroyed;
            }
        }

        private void OnSmallAsteroidDestroyed(Enemy asteroid)
        {
            _asteroidsPool.Return(asteroid as Asteroid);
            
            asteroid.OnDestroy -= OnSmallAsteroidDestroyed;
        }

        private void OnLargeAsteroidDestroyed(Enemy asteroid)
        {
            _currentLargeAsteroidsCount--;
            
            _asteroidsPool.Return(asteroid as Asteroid);
            asteroid.OnDestroy -= OnLargeAsteroidDestroyed;

            for (int i = 0; i < 2; i++)
            {
                SpawnSmall(asteroid.transform.position);
            }
        }

        private void OnDestroy()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}