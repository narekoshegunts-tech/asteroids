using System;
using Game.Scripts.Features.Player;
using TMPro;
using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class ScoreViewModel: IInitializable, IDisposable
    {
        private PlayerModel _playerModel;

        private string _scoreText;
        
        public event Action<string> OnScoreChanged;

        public ScoreViewModel(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void Initialize()
        {
            ChangeScore(0);
            _playerModel.OnScoreChanged += ChangeScore;
        }

        public void Dispose()
        {
            _playerModel.OnScoreChanged -= ChangeScore;
        }

        private void ChangeScore(int score)
        {
            _scoreText = $"Score: {score.ToString()}";
            OnScoreChanged?.Invoke(_scoreText);
        }
    }
}