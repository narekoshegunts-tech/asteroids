using System;
using Game.Scripts.Features.Player;
using Game.Scripts.UI.Views;
using MVVM;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.UI.ViewModels
{
    public class HealthViewModel: IInitializable, IDisposable
    {
        private PlayerModel _playerModel;
        
        public int MaxHealth { get; private set; }

        [field: Data("CurrentHealth")]
        public int CurrentHealth { get; private set; }
        
        public event Action<int> OnCurrentHealthChanged;

        public HealthViewModel(PlayerModel playerModel)
        {
            _playerModel = playerModel;
            MaxHealth = _playerModel.MaxHealth;
            CurrentHealth = _playerModel.CurrentHealth;
        }
        
        public void Initialize()
        {
            _playerModel.OnGetDamage += OnHealthChanged;
        }

        public void Dispose()
        {
            _playerModel.OnGetDamage -= OnHealthChanged;
        }

        private void OnHealthChanged(int currentHealth)
        {
            CurrentHealth = currentHealth;
            OnCurrentHealthChanged?.Invoke(CurrentHealth);
        }
    }
}