using System.Collections.Generic;
using Game.Scripts.Common.Services;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.Features.Player.Projectiles.Data
{
    public class ProjectilesDataService: JsonConfigLoader<ProjectileDataRoot>
    {
        private const string ResourcePath = "Configs/projectiles";
        
        private List<ProjectileData> _data;

        private Dictionary<ProjectileType, ProjectileData> _dataDict = new();
        public IReadOnlyDictionary<ProjectileType, ProjectileData> Data => _dataDict;
        
        public int BulletPoolSize { get; private set; }
        public int LaserPoolSize { get; private set; }

        public ProjectilesDataService():base(ResourcePath)
        {
            LoadConfig();
        }

        private void LoadConfig()
        {
            var root = LoadRoot();
            
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
        
        public IReadOnlyDictionary<ProjectileType, ProjectileData> GetData()
        {
            return Data;
        }
    }
}