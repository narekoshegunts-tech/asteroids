using Game.Scripts.Common.Services;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.Features.Player.Data
{
    public class PlayerDataService: JsonConfigLoader<PlayerData>
    {
        private const string ResourcePath = "Configs/player";
        
        public int MaxHealth { get; private set; }
        public float Acceleration { get; private set; }
        public int LaserAttackMaxCount { get; private set; }
        public float LaserAttackChargeTime { get; private set; }
        public float InvulnerabilityDuration { get; private set; }

        public PlayerDataService(): base(ResourcePath)
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            PlayerData data = LoadRoot();
            
            MaxHealth = data.MaxHealth;
            Acceleration = data.Acceleration;
            LaserAttackMaxCount = data.LaserAttackMaxCount;
            LaserAttackChargeTime = data.LaserAttackChargeTime;
            InvulnerabilityDuration = data.InvulnerabilityDuration;
        }
    }
}