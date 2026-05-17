using Game.Scripts.Features.Enemies.Asteroids;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies
{
    public class EnemySpawner: MonoBehaviour
    {
        [Inject] private AsteroidSpawnerService _asteroidSpawnerService;

        private void Start()
        {
            StartSpawning();
        }

        private void StartSpawning()
        {
            _asteroidSpawnerService.StartSpawning();
        }

        private void OnDestroy()
        {
            DestroySpawners();
        }

        private void DestroySpawners()
        {
            _asteroidSpawnerService.Destroy();
        }
    }
}