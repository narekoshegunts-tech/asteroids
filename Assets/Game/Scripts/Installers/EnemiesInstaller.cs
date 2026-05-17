using Game.Scripts.Features.Enemies.Asteroids;
using Game.Scripts.Features.Enemies.Asteroids.Data;
using Game.Scripts.Features.Enemies.UFO;
using Game.Scripts.Features.Enemies.UFO.Data;
using Zenject;

namespace Game.Scripts.Installers
{
    public class EnemiesInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAsteroidDataService();
            BindAsteroidSpawnerService();

            BindUfoDataService();
            BindUfoSpawnerService();
        }

        private void BindUfoSpawnerService()
        {
            Container
                .Bind<UfoSpawnerService>()
                .AsSingle()
                .NonLazy();
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
                .Bind<AsteroidSpawnerService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAsteroidDataService()
        {
            Container
                .Bind<AsteroidDataService>()
                .AsSingle()
                .NonLazy();
        }
    }
}