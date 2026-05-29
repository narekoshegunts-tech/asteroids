using System;
using Game.Scripts.Features.Player.Data;

namespace Game.Scripts.Features.Player.Model
{
    public class PlayerLaserAttackModel
    {
        public event Action<int> OnLaserAttack;
        
        public int CurrentLaserAttacks { get; private set; }
        public int MaxLaserAttacks { get; private set; }
        public float LaserChargeTime { get; private set; }
        
        public PlayerLaserAttackModel(PlayerDataService playerDataService)
        {
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