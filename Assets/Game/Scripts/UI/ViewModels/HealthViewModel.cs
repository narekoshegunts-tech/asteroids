using System;
using Game.Scripts.Features.Player.Model;
using MVVM;
using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class HealthViewModel: IInitializable, IDisposable
    {
        private PlayerHealthModel _playerHealthModel;
        
        public int MaxHealth { get; private set; }

        [field: Data("CurrentHealth")]
        public int CurrentHealth { get; private set; }
        
        public event Action<int> OnCurrentHealthChanged;

        public HealthViewModel(PlayerHealthModel playerHealthModel)
        {
            _playerHealthModel = playerHealthModel;
            MaxHealth = _playerHealthModel.MaxHealth;
            CurrentHealth = _playerHealthModel.CurrentHealth;
        }
        
        public void Initialize()
        {
            _playerHealthModel.OnHealthChanged += OnHealthChanged;
        }

        public void Dispose()
        {
            _playerHealthModel.OnHealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int currentHealth)
        {
            CurrentHealth = currentHealth;
            OnCurrentHealthChanged?.Invoke(CurrentHealth);
        }
    }
}