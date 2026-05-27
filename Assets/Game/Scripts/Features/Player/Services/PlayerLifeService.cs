using System;
using Game.Scripts.Signals;
using Zenject;

namespace Game.Scripts.Features.Player.Services
{
    public class PlayerLifeService: IDisposable
    {
        private SignalBus _signalBus;
        private PlayerModel _playerModel;

        public PlayerLifeService(PlayerModel playerModel, SignalBus signalBus)
        {
            _playerModel = playerModel;
            _signalBus = signalBus;

            _playerModel.OnDie += OnDie;
        }

        private void OnDie()
        {
            _signalBus.Fire<PlayerDiedSignal>();
        }

        public void Dispose()
        {
            _playerModel.OnDie -= OnDie;
        }
    }
}