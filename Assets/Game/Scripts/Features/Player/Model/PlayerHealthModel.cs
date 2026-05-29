using System;
using Game.Scripts.Features.Player.Data;

namespace Game.Scripts.Features.Player.Model
{
    public class PlayerHealthModel
    {
        public event Action<int> OnHealthChanged;
        public event Action OnDie;

        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }
        public float InvulnerabilityDuration { get; private set; }
        
        public PlayerHealthModel(PlayerDataService dataService)
        {
            MaxHealth = dataService.MaxHealth;
            CurrentHealth = MaxHealth;
            InvulnerabilityDuration = dataService.InvulnerabilityDuration;
        }

        public void TakeDamage()
        {
            CurrentHealth--;
            OnHealthChanged?.Invoke(CurrentHealth);

            if (CurrentHealth <= 0)
                Die();
        }

        private void Die()
        {
            OnDie?.Invoke();
        }
    }
}