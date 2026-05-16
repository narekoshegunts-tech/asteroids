using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player.Attack
{
    [RequireComponent(typeof(ProjectileMovement))]
    public abstract class Projectile: MonoBehaviour
    {
        public event Action<Projectile> OnDestroy;

        private ProjectileMovement _movement;

        protected float _lifeTime;
        
        private CancellationTokenSource _cts;
        
        
        protected void Awake()
        {
            _movement = GetComponent<ProjectileMovement>();
        }

        public virtual void Init(Vector3 startPosition, Vector2 direction)
        {
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

        protected void Destroy()
        {
            OnDestroy?.Invoke(this);
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}