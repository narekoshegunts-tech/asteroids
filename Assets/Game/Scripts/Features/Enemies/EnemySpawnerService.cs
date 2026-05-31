using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Common.CameraServices;
using Game.Scripts.Common.ObjectPool;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies
{
    public abstract class EnemySpawnerService<TEnemy> : IInitializable
        where TEnemy: Enemy
    {
        private ObjectPoolFactory _objectPoolFactory;
        
        protected CameraUtils _cameraUtils;
        
        protected ObjectPool<TEnemy> _pool;
        
        protected TEnemy _enemyPrefab;
        
        private CancellationTokenSource _cts;

        public event Action<Enemy> OnAnyEnemySpawned;
        
        protected abstract float SpawnCooldown { get; }
        protected abstract int PoolSize { get; }
        
        [Inject]
        private void Construct(ObjectPoolFactory objectPoolFactory, CameraUtils cameraUtils)
        {
            _objectPoolFactory = objectPoolFactory;
            _cameraUtils = cameraUtils;
        }
        
        public void Initialize()
        {
            GameObject container = new GameObject($"{typeof(TEnemy).Name}Pool");
            _pool = _objectPoolFactory.Create(_enemyPrefab, container, PoolSize);
        }
        
        public void StartSpawning()
        {
            StopSpawning();
            
            _cts = new CancellationTokenSource();
            _ = SpawnLoop(_cts.Token).SuppressCancellationThrow();
        }
        
        
        private async UniTask SpawnLoop(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await UniTask.Delay(TimeSpan.FromSeconds(SpawnCooldown),
                        ignoreTimeScale: false,
                        cancellationToken: _cts.Token);

                    if (CanSpawn())
                    {
                        Spawn();
                    }
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
                
            }
        }

        private void StopSpawning()
        {
            if (_cts == null)
                return;

            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        
        }

        protected void ReturnToPool(Enemy enemy)
        {
            _pool.Return(enemy as TEnemy);
        }

        protected abstract bool CanSpawn();

        protected abstract void Spawn();

        protected void RaiseOnAnyEnemySpawned(Enemy enemy)
        {
            OnAnyEnemySpawned?.Invoke(enemy);
        }

        public void Destroy()
        {
            StopSpawning();
        }
    }
}