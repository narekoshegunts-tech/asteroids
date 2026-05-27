using System;
using System.Collections.Generic;
using Game.Scripts.Features.Core.Score.Data;
using Game.Scripts.Features.Enemies;
using Game.Scripts.Features.Player;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Core.Score
{
    public class ScoreService: IInitializable, IDisposable
    {
        private PlayerModel _playerModel;
        private EnemySpawner _enemySpawner;
        
        private IReadOnlyDictionary<EnemyType, int> _enemyRewards;
        
        public int TotalScore { get; private set; }

        [Inject]
        private void Construct(ScoreDataService scoreDataService, EnemySpawner enemySpawner, PlayerModel playerModel)
        {
            TotalScore = 0;
            
            _enemyRewards = scoreDataService.Data;
            _enemySpawner = enemySpawner;
            
            _playerModel = playerModel;
        }

        public void AddScore(Enemy enemy)
        {
            TotalScore += _enemyRewards[enemy.EnemyType];
            _playerModel.ChangeScore(TotalScore);
        }

        private void OnAnyEnemySpawned(Enemy enemy)
        {
            enemy.OnDead += OnEnemyDead;
        }

        private void OnEnemyDead(Enemy enemy)
        {
            AddScore(enemy);
            enemy.OnDead -= OnEnemyDead;
        }

        public void Initialize()
        {
            _enemySpawner.OnAnyEnemySpawned += OnAnyEnemySpawned;
        }

        public void Dispose()
        {
            _enemySpawner.OnAnyEnemySpawned -= OnAnyEnemySpawned;
        }
    }
}