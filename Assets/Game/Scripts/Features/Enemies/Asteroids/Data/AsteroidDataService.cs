using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.Features.Enemies.Asteroids.Data
{
    public class AsteroidDataService
    {
        private const string ResourcePath = "Configs/asteroids";
        private List<AsteroidData> _data;

        private Dictionary<AsteroidType, AsteroidData> _dataDict = new();
        public IReadOnlyDictionary<AsteroidType, AsteroidData> Data => _dataDict;
        
        public int PoolSize { get; private set; }
        public int MaxLargeAsteroidsCount { get; private set; }
        public float LargeAsteroidSpawnCooldown { get; private set; }
        
        public int SmallAsteroidsPerLarge { get; private set; }

        public AsteroidDataService()
        {
            LoadConfig();
        }
        
        private void LoadConfig()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(ResourcePath);
            
            var root = JsonConvert.DeserializeObject<AsteroidDataRoot>(textAsset.text);

            _data = root.Asteroids;
            
            PoolSize = root.PoolSize;
            MaxLargeAsteroidsCount = root.MaxLargeAsteroidsCount;
            LargeAsteroidSpawnCooldown = root.LargeAsteroidSpawnCooldown;
            SmallAsteroidsPerLarge = root.SmallAsteroidsPerLarge;
            
            BuildDictionary();
        }
        
        private void BuildDictionary()
        {
            _dataDict.Clear();
            foreach (var item in _data)
            {
                if (_dataDict.ContainsKey(item.Type))
                    Debug.LogWarning($"Дубликат в конфиге астероидов");
                else
                    _dataDict[item.Type] = item;
            }
        }
        
    }
}