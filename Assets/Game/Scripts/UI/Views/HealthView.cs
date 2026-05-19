using System.Collections.Generic;
using Game.Scripts.Features.Player;
using Game.Scripts.UI.Game.PlayerCanvas;
using UnityEngine;
using Zenject;

using MVVM;

namespace Game.Scripts.UI.Views
{
    public class HealthView: MonoBehaviour
    {
        [SerializeField] private Heart _heartPrefab;
        private List<Heart> _hearts = new List<Heart>();
        
        private int _maxHealth;
        
        private int _currentHealth;

        public void Initialize(int maxHealth)
        {
            Debug.Log(maxHealth);
            _maxHealth = maxHealth;

            for (int i = 0; i < _maxHealth; i++)
            {
                _hearts.Add(Instantiate(_heartPrefab, transform).GetComponent<Heart>());
            }
        }


        [Method("CurrentHealth")]
        public void UpdateHearts(int currentHealth)
        {
            _currentHealth = currentHealth;
            _hearts[_currentHealth].Disapear();
        }
    }
}