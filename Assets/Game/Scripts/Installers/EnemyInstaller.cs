using Game.Scripts.Features.Enemies;
using Game.Scripts.Features.Enemies.Asteroids;
using Game.Scripts.Features.Enemies.Asteroids.Data;
using Game.Scripts.Features.Enemies.Ufo;
using Game.Scripts.Features.Enemies.Ufo.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class EnemyInstaller: MonoInstaller
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        public override void InstallBindings()
        {
            BindAsteroidDataService();
            BindAsteroidSpawnerService();
            BindUfoDataService();
            BindUfoSpawnerService();
            BindEnemySpawner();
        }

        private void BindEnemySpawner()
        {
            Container
                .Bind<EnemySpawner>()
                .FromComponentInNewPrefab(_enemySpawner)
                .AsSingle();
        }

        private void BindUfoSpawnerService()
        {
            Container
                .BindInterfacesAndSelfTo<UfoSpawnerService>()
                .AsSingle();
        }

        private void BindUfoDataService()
        {
            Container
                .Bind<UfoDataService>()
                .AsSingle();
        }
        private void BindAsteroidSpawnerService()
        {
            Container
                .BindInterfacesAndSelfTo<AsteroidSpawnerService>()
                .AsSingle();
        }

        private void BindAsteroidDataService()
        {
            Container
                .BindInterfacesAndSelfTo<AsteroidDataService>()
                .AsSingle();
        }
    }
}