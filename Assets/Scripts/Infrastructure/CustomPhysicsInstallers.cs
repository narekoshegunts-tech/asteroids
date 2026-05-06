using Zenject;
using CustomPhysics;
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
            BindCustomPhysicsFacade2DFactory();
        }

        private void BindPosition2DFactory()
        {
            Container
                .BindFactory<Transform, Velocity2D, Position2D, Position2D.Facory>();
        }
        private void BindVelocity2DFactory()
        {
            Container
                .BindFactory<Acceleration2D, Velocity2D, Velocity2D.Factory>();
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
                .BindFactory<Transform, CustomPhysicsFacade2D, CustomPhysicsFacade2D.Factory>();
        }
        
    }
}