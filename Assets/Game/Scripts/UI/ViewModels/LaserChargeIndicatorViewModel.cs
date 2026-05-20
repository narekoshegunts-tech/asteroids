using System;
using Game.Scripts.Features.Player;
using MVVM;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class LaserChargeIndicatorViewModel: IInitializable, IDisposable
    {
        private PlayerModel _playerModel;
        
        public int MaxLaserAttacks { get; private set; }

        [field: Data("CurrentLaserAttacks")]
        public int CurrentLaserAttacks { get; private set; }
        
        public float ChargeTime { get; private set; }
        
        public event Action<int> OnLaserAttack;

        public LaserChargeIndicatorViewModel(PlayerModel playerModel)
        {
            _playerModel = playerModel;
            MaxLaserAttacks = _playerModel.MaxLaserAttacks;
            CurrentLaserAttacks = _playerModel.CurrentLaserAttacks;
            ChargeTime = _playerModel.LaserChargeTime;
        }
        
        public void Initialize()
        {
            _playerModel.OnLaserAttack += OnCurrentLaserAttackChanged;
        }

        public void Dispose()
        {
            _playerModel.OnLaserAttack -= OnCurrentLaserAttackChanged;
        }

        private void OnCurrentLaserAttackChanged(int currentLaserAttacks)
        {
            OnLaserAttack?.Invoke(currentLaserAttacks);
        }

        public void OnLaserCharge(int currentLaserAttacks)
        {
            _playerModel.LaserAttackCharge(currentLaserAttacks);
        }
    }
}