using System;
using Game.Scripts.Features.Player.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player.Model
{
    public class PlayerModel
    {
        public event Action<int> OnLaserAttack;
        public event Action<Vector2> OnPositionChanged;
        public event Action<float> OnRotationChanged;
        public event Action <float> OnVelocityChanged;
        
        
        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }
        public float Velocity { get; private set; }
        
        public int CurrentLaserAttacks { get; private set; }
        public int MaxLaserAttacks { get; private set; }
        public float LaserChargeTime { get; private set; }
        
        private PlayerHealthModel _playerHealthModel;

        [Inject]
        public PlayerModel(PlayerDataService playerDataService,
            PlayerHealthModel playerHealthModel)
        {
            _playerHealthModel = playerHealthModel;
            
            Position = Vector2.zero;
            Rotation = 0;
            Velocity = 0;
            
            MaxLaserAttacks = playerDataService.LaserAttackMaxCount;
            CurrentLaserAttacks = MaxLaserAttacks;
            LaserChargeTime = playerDataService.LaserAttackChargeTime;
        }
        

        public bool TryLaserAttack()
        {
            if (CurrentLaserAttacks == 0)
                return false;
            
            CurrentLaserAttacks--;
            OnLaserAttack?.Invoke(CurrentLaserAttacks);
            return true;
        }

        public void SetLaserCharges(int currentLaserAttacks)
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
    }
}