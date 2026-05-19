
using System;
using Game.Scripts.Features.Player.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerModel
    {
        public event Action<int> OnGetDamage;
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        
        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }
        public float Velocity { get; private set; }

        [Inject]
        public PlayerModel(PlayerDataService playerDataService)
        {
            CurrentHealth = playerDataService.MaxHealth;
            MaxHealth = playerDataService.MaxHealth;
            Position = Vector2.zero;
            Rotation = 0;
            Velocity = 0;
        }

        public void GetDamage()
        {
            CurrentHealth--;
            OnGetDamage?.Invoke(CurrentHealth);
        }
    }
}