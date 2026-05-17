using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Common.CameraServices;
using Game.Scripts.Common.ObjectPool;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies
{
    public abstract class EnemySpawnerService<TEnemy> 
        where TEnemy: Enemy
    {
        [Inject] protected CameraUtils _cameraService;
        
        protected ObjectPool<TEnemy> _pool;
        
        protected TEnemy _enemyPrefab;
        
        private CancellationTokenSource _cts;
        
        protected abstract string PrefabPath { get; }
        protected abstract float SpawnCooldown { get; }
        protected abstract int PoolSize { get; }
        
        [Inject]
        protected virtual void Construct(ObjectPoolFactory objectPoolFactory)
        {
            _enemyPrefab = Resources.Load<TEnemy>(PrefabPath);
            
            GameObject container = new GameObject($"{typeof(TEnemy).Name}Pool");
            _pool = objectPoolFactory.Create(_enemyPrefab, container, PoolSize);
        }
        
        public void StartSpawning()
        {
            _cts = new CancellationTokenSource();
            SpawnLoop().Forget();
        }
        
        
        private async UniTaskVoid SpawnLoop()
        {
            while (_cts.IsCancellationRequested == false)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(SpawnCooldown),
                    ignoreTimeScale: false,
                    cancellationToken: _cts.Token);
                
                if (CanSpawn())
                {
                    Spawn();    
                }
            }
        }

        protected void ReturnToPool(Enemy enemy)
        {
            _pool.Return(enemy as TEnemy);
        }

        protected abstract bool CanSpawn();

        protected abstract void Spawn();

        public void Destroy()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}