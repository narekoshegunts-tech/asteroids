using Zenject;
using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using UnityEngine;

namespace Infrastructure
{
    public class CustomPhysicsInstallers: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAcceleration2D();
            BindVelocity2DFactory();
            BindPosition2DFactory();
            BindRotation2DFactory();
            BindCustomPhysicsFacade2DFactory();
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