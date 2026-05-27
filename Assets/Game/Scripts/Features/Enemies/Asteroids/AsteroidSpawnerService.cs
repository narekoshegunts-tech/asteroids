using System.Collections.Generic;
using Game.Scripts.Features.Enemies.Asteroids.Data;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Features.Enemies.Asteroids
{
    public class AsteroidSpawnerService: EnemySpawnerService<Asteroid>
    {
        private AsteroidDataService _asteroidsDataService;
        
        private Dictionary<AsteroidType, AsteroidData> _asteroidsData;

        private int _maxLargeAsteroidsCount;
        private int _currentLargeAsteroidsCount;
        private int _smallAsteroidsPerLarge;
        
        protected override string PrefabPath => "Prefabs/Enemies/Asteroid";
        protected override float SpawnCooldown => _asteroidsDataService.LargeAsteroidSpawnCooldown;
        protected override int PoolSize => _asteroidsDataService.PoolSize;
        
        [Inject]
        private void Construct(AsteroidDataService asteroidsDataService)
        {
            _asteroidsDataService = asteroidsDataService;
            
            _maxLargeAsteroidsCount = _asteroidsDataService.MaxLargeAsteroidsCount;
            _smallAsteroidsPerLarge = _asteroidsDataService.SmallAsteroidsPerLarge;
            
            _asteroidsData = _asteroidsDataService.GetData();
        }

        protected override bool CanSpawn()
        {
            return _currentLargeAsteroidsCount < _maxLargeAsteroidsCount;
        }

        protected override void Spawn()
        {
            if (_pool.TryGet(out Asteroid asteroid))
            {
                Vector2 spawnPosition = _cameraUtils.GetOffscreenPosition();
                Vector2 targetPosition = _cameraUtils.GetScreenRandomPosition();
                asteroid.Initialize(spawnPosition, targetPosition, _asteroidsData[AsteroidType.Large]);
                
                RaiseOnAnyEnemySpawned(asteroid);
                asteroid.OnDead += OnLargeAsteroidDestroyed;
                
                _currentLargeAsteroidsCount++;
            }
        }

        private void SpawnSmall(Vector2 spawnPosition, float offset = 1)
        {
            if (_pool.TryGet(out Asteroid asteroid))
            {
                Vector2 vectorOffset = new Vector2(Random.Range(-offset, offset), Random.Range(-offset, offset));
                Vector2 targetPosition = _cameraUtils.GetScreenRandomPosition();
                asteroid.Initialize(spawnPosition + vectorOffset, targetPosition, _asteroidsData[AsteroidType.Small]);

                asteroid.OnDead += OnSmallAsteroidDestroyed;
                RaiseOnAnyEnemySpawned(asteroid);
            }
        }

        private void OnSmallAsteroidDestroyed(Enemy asteroid)
        {
            ReturnToPool(asteroid);
            
            asteroid.OnDead -= OnSmallAsteroidDestroyed;
        }

        private void OnLargeAsteroidDestroyed(Enemy asteroid)
        {
            _currentLargeAsteroidsCount--;

            ReturnToPool(asteroid);
            asteroid.OnDead -= OnLargeAsteroidDestroyed;

            for (int i = 0; i < _smallAsteroidsPerLarge; i++)
            {
                SpawnSmall(asteroid.transform.position);
            }
        }
    }
}