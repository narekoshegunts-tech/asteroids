using Game.Scripts.Common.ObjectPool;
using Game.Scripts.Features.Player.Projectiles;
using Game.Scripts.Features.Player.Projectiles.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player.Services
{
    public abstract class ProjectileAttackService<T> where T: Projectile
    {
        private readonly string _prefabPath;
        private readonly string _poolName;

        protected ObjectPool<T> _pool;
        protected GameObject _poolContainer;
        protected T _prefab;
        
        protected ProjectileAttackService()
        {
            _prefabPath = GetPrefabPath();
            _poolName = GetPoolName();
        }
        
        protected abstract int GetPoolSize(ProjectilesDataService projectilesDataService);
        protected abstract string GetPoolName();
        protected abstract string GetPrefabPath();
        
        [Inject]
        protected void Construct(ObjectPoolFactory objectPoolFactory, 
            ProjectilesDataService projectilesDataService)
        {
            _prefab = Resources.Load<T>(_prefabPath);
            
            _poolContainer = new GameObject(_poolName);
            int poolSize = GetPoolSize(projectilesDataService);
            
            _pool = objectPoolFactory.Create(_prefab, _poolContainer, poolSize);
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