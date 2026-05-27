using System;
using System.Collections.Generic;
using Game.Scripts.Features.Core.Score.Data;
using Game.Scripts.Features.Enemies;
using Zenject;

namespace Game.Scripts.Features.Core.Score
{
    public class ScoreService: IInitializable, IDisposable
    {
        private EnemySpawner _enemySpawner;
        
        private IReadOnlyDictionary<EnemyType, int> _enemyRewards;

        public event Action<int> OnScoreChanged;
        public int TotalScore { get; private set; }

        [Inject]
        private void Construct(ScoreDataService scoreDataService, EnemySpawner enemySpawner)
        {
            TotalScore = 0;
            
            _enemyRewards = scoreDataService.Data;
            _enemySpawner = enemySpawner;
        }

        public void AddScore(Enemy enemy)
        {
            TotalScore += _enemyRewards[enemy.EnemyType];
            OnScoreChanged?.Invoke(TotalScore);
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