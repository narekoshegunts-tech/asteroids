using System.Collections.Generic;
using Game.Scripts.Features.Core.Score.Data;
using Game.Scripts.Features.Enemies;
using Game.Scripts.Features.Player;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Core.Score
{
    public class ScoreManager: MonoBehaviour
    {
        [Inject] private PlayerModel _playerModel;
        private EnemySpawner _enemySpawner;
        
        private Dictionary<EnemyType, int> _enemyRewards;
        
        public int TotalScore { get; private set; }

        [Inject]
        private void Construct(ScoreDataService scoreDataService, EnemySpawner enemySpawner)
        {
            _enemyRewards = scoreDataService.GetData();
            _enemySpawner = enemySpawner;
            _enemySpawner.OnAnyEnemySpawned += enemy => enemy.OnDestroy += AddScore;
        }
        
        private void Awake()
        {
            TotalScore = 0;
        }

        public void AddScore(Enemy enemy)
        {
            TotalScore += _enemyRewards[enemy.EnemyType];
            Debug.Log(TotalScore);
        }
    }
}