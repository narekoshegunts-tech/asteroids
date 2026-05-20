
using System;
using Game.Scripts.Features.Player.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerModel
    {
        public event Action<int> OnGetDamage;
        public event Action<int> OnLaserAttack;
        public event Action<Vector2> OnPositionChanged;
        public event Action<float> OnRotationChanged;
        public event Action <float> OnVelocityChanged;
        public event Action<int> OnScoreChanged;
        
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        
        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }
        public float Velocity { get; private set; }
        
        public int CurrentLaserAttacks { get; private set; }
        public int MaxLaserAttacks { get; private set; }
        public float LaserChargeTime { get; private set; }
        
        public int TotalScore { get; private set; }

        [Inject]
        public PlayerModel(PlayerDataService playerDataService)
        {
            CurrentHealth = playerDataService.MaxHealth;
            MaxHealth = playerDataService.MaxHealth;
            Position = Vector2.zero;
            Rotation = 0;
            Velocity = 0;
            
            MaxLaserAttacks = playerDataService.LaserAttackMaxCount;
            CurrentLaserAttacks = MaxLaserAttacks;
            LaserChargeTime = playerDataService.LaserAttackChargeTime;
        }

        public void GetDamage()
        {
            CurrentHealth--;
            OnGetDamage?.Invoke(CurrentHealth);
        }

        public bool TryLaserAttack()
        {
            if (CurrentLaserAttacks == 0)
                return false;
            
            CurrentLaserAttacks--;
            OnLaserAttack?.Invoke(CurrentLaserAttacks);
            return true;
        }

        public void LaserAttackCharge(int currentLaserAttacks)
        {
            CurrentLaserAttacks = currentLaserAttacks;
        }

        public void ChangePosition(Vector2 position)
        {
            Position = position;
            OnPositionChanged?.Invoke(position);
        }

        public void ChangeRotation(float rotation)
        {
            Rotation = rotation;
            OnRotationChanged?.Invoke(rotation);
        }

        public void ChangeVelocity(float velocity)
        {
            Velocity = velocity;
            OnVelocityChanged?.Invoke(velocity);
        }

        public void ChangeScore(int score)
        {
            TotalScore = score;
            OnScoreChanged?.Invoke(TotalScore);
        }
    }
}