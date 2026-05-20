using System;
using UnityEngine;

namespace Game.Scripts.Features.Enemies
{
    public abstract class Enemy: MonoBehaviour
    {
        public event Action<Enemy> OnDestroy;
        
        public EnemyType EnemyType { get; protected set; }

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
        }
    }
}