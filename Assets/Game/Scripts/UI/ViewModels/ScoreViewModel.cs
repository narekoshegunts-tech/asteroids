using System;
using Game.Scripts.Features.Core.Score;
using Game.Scripts.Features.Player;
using TMPro;
using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class ScoreViewModel: IInitializable, IDisposable
    {
        private ScoreService _scoreService;

        private string _scoreText;
        
        public event Action<string> OnScoreChanged;

        public ScoreViewModel(ScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        public void Initialize()
        {
            ChangeScore(_scoreService.TotalScore);
            _scoreService.OnScoreChanged += ChangeScore;
        }

        public void Dispose()
        {
            _scoreService.OnScoreChanged -= ChangeScore;
        }

        private void ChangeScore(int score)
        {
            _scoreText = $"Score: {score.ToString()}";
            OnScoreChanged?.Invoke(_scoreText);
        }
    }
}