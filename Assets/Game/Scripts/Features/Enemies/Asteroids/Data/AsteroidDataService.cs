// AsteroidDataService.cs

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies.Asteroids.Data
{
    public class AsteroidDataService
    {
        private const string ResourcePath = "Configs/asteroids";
        private List<AsteroidData> _data;

        private Dictionary<AsteroidType, AsteroidData> _dataDict = new();
        
        public int PoolSize { get; private set; }

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

        public Dictionary<AsteroidType,AsteroidData> GetData()
        {
            return new Dictionary<AsteroidType, AsteroidData>(_dataDict);
        }
    }
}