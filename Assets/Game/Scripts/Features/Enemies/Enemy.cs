using System;
using UnityEngine;

namespace Game.Scripts.Features.Enemies
{
    public abstract class Enemy: MonoBehaviour
    {
        public event Action<Enemy> OnDead;
        
        public EnemyType EnemyType { get; protected set; }

        public void Die()
        {
            OnDead?.Invoke(this);
        }
    }
}