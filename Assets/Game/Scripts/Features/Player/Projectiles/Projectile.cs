using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Features.Enemies;
using Game.Scripts.Features.Player.Projectiles.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player.Projectiles
{
    [RequireComponent(typeof(ProjectileMovement))]
    public abstract class Projectile: MonoBehaviour
    {
        public event Action<Projectile> OnDestroy;

        protected ProjectileMovement _movement;

        protected float _lifeTime;
        
        private CancellationTokenSource _cts;
        
        protected abstract void Construct(ProjectilesDataService dataService);
        
        protected void Awake()
        {
            _movement = GetComponent<ProjectileMovement>();
        }

        public void Init(Vector3 startPosition, Vector2 direction)
        {
            CleanupCts();
            _cts = new CancellationTokenSource();
            
            _movement.Init(startPosition, direction);

            DestroyAfterLifeTime().Forget();
        }

        private async UniTask DestroyAfterLifeTime()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_lifeTime),
                cancellationToken: _cts.Token);
            
            Destroy();
        }

        private void CleanupCts()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        protected void Destroy()
        {
            CleanupCts();
            OnDestroy?.Invoke(this);
        }
    }
}