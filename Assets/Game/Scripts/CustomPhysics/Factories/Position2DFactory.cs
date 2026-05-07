using UnityEngine;
using Zenject;

namespace Game.Scripts.CustomPhysics.Factories
{
    public class Position2DFactory
    {

        private readonly DiContainer _container;

        public Position2DFactory(DiContainer container)
        {
            _container = container;
        }

        public Position2D Create(Transform transform, Velocity2D velocity2D)
        {
            return _container.Instantiate<Position2D>(new object[] { transform, velocity2D });
        }
    }
}