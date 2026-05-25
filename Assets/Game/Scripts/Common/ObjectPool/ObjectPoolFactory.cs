using UnityEngine;
using Zenject;

namespace Game.Scripts.Common.ObjectPool
{
    public class ObjectPoolFactory
    {
        private readonly DiContainer _container;
        
        public ObjectPoolFactory(DiContainer container)
        {
            _container = container;
        }
        public ObjectPool<T> Create<T>(T prefab, GameObject poolContainer, int size) where T : Component
        {
            return _container.Instantiate<ObjectPool<T>>(new object[] {prefab, poolContainer, size, _container});
        }
        
    }
}