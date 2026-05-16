using UnityEngine;
using Zenject;

namespace Game.Scripts.CustomPhysics.Factories
{
    public class CustomPhysicsFacade2DFactory
    {
        private readonly DiContainer _container;
        
        public CustomPhysicsFacade2DFactory(DiContainer container)
        {
            _container = container;
        }
        
        public CustomPhysicsFacade2D Create(Transform transform, float mass = 1)
        {
            var facade = _container.Instantiate<CustomPhysicsFacade2D>(new object[] { transform, mass });
            return facade;
        }
    }
}