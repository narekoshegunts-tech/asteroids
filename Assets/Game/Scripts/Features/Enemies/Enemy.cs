using System;
using UnityEngine;

namespace Game.Scripts.Features.Enemies
{
    public abstract class Enemy: MonoBehaviour
    {
        public event Action<Enemy> OnDestroy;

        public void Destroy()
        {
            OnDestroy?.Invoke(this);
        }
    }
}