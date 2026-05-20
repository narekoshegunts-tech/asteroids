using System;
using Game.Scripts.Signals;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Scripts.UI.Game.Services
{
    public class GameFlowController: IInitializable, IDisposable
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private GamePauseService _gamePauseService;
        [Inject] private EndGame _endGame;
        
        public void Initialize()
        {
            _signalBus.Subscribe<PlayerDiedSignal>(HandlePlayerDeath);
            _signalBus.Subscribe<RestartRequestSignal>(RestartGame);
        }
        
        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(HandlePlayerDeath);
            _signalBus.Unsubscribe<RestartRequestSignal>(RestartGame);
        }

        private void HandlePlayerDeath()
        {
            _gamePauseService.Pause();
            _endGame.Show();
        }

        private void RestartGame()
        {
            _gamePauseService.Resume();
            
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.buildIndex);
        }

        
    }
}