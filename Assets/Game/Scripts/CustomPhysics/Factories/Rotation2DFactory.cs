using UnityEngine;
using Zenject;

namespace Game.Scripts.CustomPhysics.Factories
{
    public class Rotation2DFactory
    {
        private readonly DiContainer _container;

        public Rotation2DFactory(DiContainer container)
        {
            _container = container;
        }

        public Rotation2D Create(Transform transform)
        {
            return _container.Instantiate<Rotation2D>(new object[] { transform });
        }
    }
}