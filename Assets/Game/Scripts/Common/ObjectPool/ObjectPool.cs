using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Common.ObjectPool
{
    public class ObjectPool<T> where T : Component
    {
        private GameObject _container;
        private T _prefab;
        
        private Stack<T> _pool;
        private int _size;

        // хз плохо ли передавать dicontainer. Другого решения не нашел
        public ObjectPool(T prefab,GameObject container,int size, DiContainer diContainer)
        {
            _prefab = prefab;
            _container = container;
            _size = size;
            _pool = new Stack<T>(_size);

            for (int i = 0; i < _size; i++)
            {
                var obj = Create(diContainer);
                _pool.Push(obj);
            }
        }

        private T Create(DiContainer diContainer)
        {
            var obj = diContainer.InstantiatePrefabForComponent<T>(_prefab, _container.transform);
            obj.gameObject.SetActive(false);
            return obj;
        }

        public bool TryGet(out T obj)
        {
            obj = null;
            if (_pool.Count > 0)
            {
                obj = _pool.Pop();
                obj.gameObject.SetActive(true);
                return true;
            }

            return false;
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            _pool.Push(obj);
        }
    }
}