using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using Zenject;

namespace Game.Scripts.Installers
{
    public class CustomPhysicsInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAcceleration2D();
            BindVelocity2DFactory();
            BindPosition2DFactory();
            BindRotation2DFactory();
            BindCustomPhysicsFacade2DFactory();
            BindSimulateCollisionService();

        }

        private void BindSimulateCollisionService()
        {
            Container
                .Bind<SimulateCollisionService>()
                .AsSingle();
        }

        private void BindRotation2DFactory()
        {
            Container.Bind<Rotation2DFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPosition2DFactory()
        {
            Container
                .Bind<Position2DFactory>()
                .AsSingle()
                .NonLazy();
        }
        private void BindVelocity2DFactory()
        {
            Container
                .Bind<Velocity2DFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void BindAcceleration2D()
        {
            Container
                .Bind<Acceleration2D>()
                .AsTransient();
        }
        
        private void BindCustomPhysicsFacade2DFactory()
        {
            Container
                .Bind<CustomPhysicsFacade2DFactory>()
                .AsSingle()
                .NonLazy();
        }
        
    }
}