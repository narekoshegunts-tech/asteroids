using System;
using Game.Scripts.Features.Enemies.Asteroids;
using Game.Scripts.Features.Enemies.UFO;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies
{
    public class EnemySpawner: MonoBehaviour
    {
        private AsteroidSpawnerService _asteroidSpawnerService;
        private UfoSpawnerService _ufoSpawnerService;

        public event Action<Enemy> OnAnyEnemySpawned;

        [Inject]
        private void Construct(AsteroidSpawnerService asteroidSpawnerService, UfoSpawnerService ufoSpawnerService)
        {
            _asteroidSpawnerService = asteroidSpawnerService;
            _ufoSpawnerService = ufoSpawnerService;
        }
        private void Start()
        {
            StartSpawning();
            SubscribeToSpawners();
        }

        private void SubscribeToSpawners()
        {
            _asteroidSpawnerService.OnAnyEnemySpawned += HandleEnemySpawn;
            _ufoSpawnerService.OnAnyEnemySpawned += HandleEnemySpawn;
        }

        private void HandleEnemySpawn(Enemy enemy)
        {
            OnAnyEnemySpawned?.Invoke(enemy);
        }

        private void StartSpawning()
        {
            _ufoSpawnerService.StartSpawning();
            _asteroidSpawnerService.StartSpawning();
        }

        private void OnDestroy()
        {
            DestroySpawners();
        }

        private void DestroySpawners()
        {
            _ufoSpawnerService.Destroy();
            _asteroidSpawnerService.Destroy();
        }
    }
}