using Zenject;

namespace Game.Scripts.CustomPhysics.Factories
{
    public class Velocity2DFactory
    {
        private readonly DiContainer _container;

        public Velocity2DFactory(DiContainer container)
        {
            _container = container;
        }

        public Velocity2D Create(Acceleration2D acceleration2D)
        {
            return _container.Instantiate<Velocity2D>(new object[] { acceleration2D });
        }
    }
}