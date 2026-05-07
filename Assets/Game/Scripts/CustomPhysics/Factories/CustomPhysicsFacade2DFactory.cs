using UnityEngine;
using Zenject;

namespace Game.Scripts.CustomPhysics.Factories
{
    public class CustomPhysicsFacade2DFactory: IFactory<Transform,CustomPhysicsFacade2D>
    {
        private readonly DiContainer _container;
        
        public CustomPhysicsFacade2DFactory(DiContainer container)
        {
            _container = container;
        }
        
        public CustomPhysicsFacade2D Create(Transform transform)
        {
            var facade = _container.Instantiate<CustomPhysicsFacade2D>(new object[] { transform });
            return facade;
        }
    }
}