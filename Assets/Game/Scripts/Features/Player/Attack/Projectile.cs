using System;
using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player.Attack
{
    public abstract class Projectile: MonoBehaviour
    {
        public event Action<Projectile> OnDestroy;
        
        protected CustomPhysicsFacade2D _customPhysicsFacade2D;

        protected float _lifeTime;
        protected float _currentLifeTime;
        
        [SerializeField] private float _speed; 
        
        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacade2DFactory)
        {
            _customPhysicsFacade2D = customPhysicsFacade2DFactory.Create(transform);
        }

        public virtual void Init(Vector3 startPosition, Vector2 direction)
        {
            _currentLifeTime = 0;
            _customPhysicsFacade2D.ApplyPosition(startPosition);
            _customPhysicsFacade2D.ApplyRotation(direction);
                        
            _customPhysicsFacade2D.ApplyVelocity(_speed);
        }
        
        protected void Update()
        {
            _currentLifeTime += Time.deltaTime;
            if (_currentLifeTime >= _lifeTime)
            {
                Destroy();
            }
        }

        protected void FixedUpdate()
        {
            _customPhysicsFacade2D.FixedUpdate();
        }

        protected void Destroy()
        {
            OnDestroy?.Invoke(this);
        }
        
    }
}