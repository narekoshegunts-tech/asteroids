using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.Features.Enemies.UFO.Data
{
    public class UfoDataService
    {
        private const string ResourcePath = "Configs/ufo";
        
        
        public int PoolSize { get; private set; }
        public float SpawnCooldown { get; private set; }

        public UfoData UfoData { get; private set; }
        
        public UfoDataService()
        {
            LoadConfig();
        }
        
        private void LoadConfig()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(ResourcePath);
            
            var root = JsonConvert.DeserializeObject<UfoDataRoot>(textAsset.text);

            UfoData = root.UfoData;
            
            PoolSize = root.PoolSize;
            
            SpawnCooldown = root.SpawnCooldown;
        }
        
    }
}