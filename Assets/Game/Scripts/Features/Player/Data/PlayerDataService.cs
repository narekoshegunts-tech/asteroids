using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.Features.Player.Data
{
    public class PlayerDataService
    {
        private const string ResourcePath = "Configs/player";
        
        public int MaxHealth { get; private set; }
        public float Acceleration { get; private set; }
        public int LaserAttackMaxCount { get; private set; }
        public float LaserAttackChargeTime { get; private set; }

        public PlayerDataService()
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            TextAsset json = Resources.Load<TextAsset>(ResourcePath);
            PlayerData data = JsonConvert.DeserializeObject<PlayerData>(json.text);
            
            MaxHealth = data.MaxHealth;
            Acceleration = data.Acceleration;
            LaserAttackMaxCount = data.LaserAttackMaxCount;
            LaserAttackChargeTime = data.LaserAttackChargeTime;
        }
    }
}