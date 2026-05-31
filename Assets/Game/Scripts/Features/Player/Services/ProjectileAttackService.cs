using Game.Scripts.Common.ObjectPool;
using Game.Scripts.Features.Player.Projectiles;
using Game.Scripts.Features.Player.Projectiles.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player.Services
{
    public abstract class ProjectileAttackService<T> where T: Projectile
    {
        private readonly string _poolName;

        private ObjectPoolFactory _poolFactory;
        
        protected ObjectPool<T> _pool;
        protected GameObject _poolContainer;
        protected T _prefab;
        
        private int _poolSize;
        
        protected ProjectileAttackService()
        {
            _poolName = GetPoolName();
        }
        
        protected abstract int GetPoolSize(ProjectilesDataService projectilesDataService);
        protected abstract string GetPoolName();
        
        [Inject]
        private void Construct(ObjectPoolFactory objectPoolFactory, 
            ProjectilesDataService projectilesDataService)
        {
            
            _poolContainer = new GameObject(_poolName);
            _poolSize = GetPoolSize(projectilesDataService);
            
            _poolFactory = objectPoolFactory;
        }

        public void Initialize()
        {
            _pool = _poolFactory.Create(_prefab, _poolContainer, _poolSize);
        }
        
        
        public void Attack(Vector2 attackPosition, Vector2 direction)
        {
            if (_pool.TryGet(out var projectile))
            {
                projectile.Init(attackPosition, direction);
                projectile.gameObject.SetActive(true);
                projectile.OnDestroy += ReturnToPool;
            }
        }
        
        private void ReturnToPool(Projectile projectile)
        {
            if (projectile is T typedProjectile)
            {
                _pool.Return(typedProjectile);
            }
            
            projectile.OnDestroy -= ReturnToPool;
        }
    }
}