using System;
using Game.Scripts.Features.Player.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player.Model
{
    public class PlayerModel
    {
        public event Action<int> OnLaserAttack;
        
        public int CurrentLaserAttacks { get; private set; }
        public int MaxLaserAttacks { get; private set; }
        public float LaserChargeTime { get; private set; }
        
        private PlayerHealthModel _playerHealthModel;
        private PlayerMovementModel _playerMovementModel;
        
        public PlayerModel(PlayerDataService playerDataService,
            PlayerHealthModel playerHealthModel, PlayerMovementModel playerMovementModel)
        {
            _playerHealthModel = playerHealthModel;
            _playerMovementModel = playerMovementModel;
            
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
    }
}