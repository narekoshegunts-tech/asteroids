using Game.Scripts.Features.Enemies.Asteroids.Data;
using Zenject;

namespace Game.Scripts.Installers
{
    public class EnemiesInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAsteroidDataService();
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