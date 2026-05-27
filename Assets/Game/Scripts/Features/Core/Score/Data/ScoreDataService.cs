using System.Collections.Generic;
using Game.Scripts.Features.Enemies;
using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.Features.Core.Score.Data
{
    public class ScoreDataService
    {
        private const string ResourcePath = "Configs/scores";

        private List<ScoreData> _scores;
        
        private Dictionary<EnemyType, int> _dataDict = new();
        public IReadOnlyDictionary<EnemyType, int> Data => _dataDict;

        public ScoreDataService()
        {
            LoadConfig();
        }
        
        private void LoadConfig()
        {
            TextAsset config = Resources.Load<TextAsset>(ResourcePath);
            
            ScoreDataRoot root = JsonConvert.DeserializeObject<ScoreDataRoot>(config.text);
            _scores = root.Scores;
            
            BuildDictionary();
        }

        private void BuildDictionary()
        {
            _dataDict.Clear();
            foreach (var item in _scores)
            {
                if (_dataDict.ContainsKey(item.Type))
                    Debug.LogWarning($"Дубликат в конфиге наград за врагов");
                else
                    _dataDict[item.Type] = item.Score;
            }
        }

        public Dictionary<EnemyType, int> GetData()
        {
            return new Dictionary<EnemyType, int>(_dataDict);
        }
    }
}