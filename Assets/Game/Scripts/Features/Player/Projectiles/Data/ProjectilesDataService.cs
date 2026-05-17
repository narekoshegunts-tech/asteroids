using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.Features.Player.Projectiles.Data
{
    public class ProjectilesDataService
    {
        private const string ResourcePath = "Configs/projectiles";
        
        private List<ProjectileData> _data;

        private Dictionary<ProjectileType, ProjectileData> _dataDict = new();
        
        public int BulletPoolSize { get; private set; }
        public int LaserPoolSize { get; private set; }

        public ProjectilesDataService()
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            TextAsset config = Resources.Load<TextAsset>(ResourcePath);

            ProjectileDataRoot root = JsonConvert.DeserializeObject<ProjectileDataRoot>(config.text);
            
            _data = root.Projectiles;
            BulletPoolSize = root.BulletPoolSize;
            LaserPoolSize = root.LaserPoolSize;
            
            BuildDictionary();
        }
        
        private void BuildDictionary()
        {
            _dataDict.Clear();
            foreach (var item in _data)
            {
                if (_dataDict.ContainsKey(item.Type))
                    Debug.LogWarning($"Дубликат в конфиге снарядов");
                else
                    _dataDict[item.Type] = item;
            }
        }
        
        public Dictionary<ProjectileType, ProjectileData> GetData()
        {
            return new Dictionary<ProjectileType, ProjectileData>(_dataDict);
        }
    }
}