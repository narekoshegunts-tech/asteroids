using System;
using Game.Scripts.Features.Player.Model;
using Game.Scripts.Signals;
using Zenject;

namespace Game.Scripts.Features.Player.Services
{
    public class PlayerLifeService: IDisposable
    {
        private SignalBus _signalBus;
        private PlayerHealthModel _playerHealthModel;

        public PlayerLifeService(SignalBus signalBus, PlayerHealthModel playerHealthModel)
        {
            _signalBus = signalBus;
            
            _playerHealthModel = playerHealthModel;

            _playerHealthModel.OnDie += OnDie;
        }

        private void OnDie()
        {
            _signalBus.Fire<PlayerDiedSignal>();
        }

        public void Dispose()
        {
            _playerHealthModel.OnDie -= OnDie;
        }
    }
}