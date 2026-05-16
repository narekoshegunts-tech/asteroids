using System;
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
        protected float _currentLifeTime;
        
        
        protected void Awake()
        {
            _movement = GetComponent<ProjectileMovement>();
        }

        public virtual void Init(Vector3 startPosition, Vector2 direction)
        {
            _currentLifeTime = 0;
            _movement.Init(startPosition, direction);
        }
        
        protected void Update()
        {
            _currentLifeTime += Time.deltaTime;
            if (_currentLifeTime >= _lifeTime)
            {
                Destroy();
            }
        }

        protected void Destroy()
        {
            OnDestroy?.Invoke(this);
        }
    }
}