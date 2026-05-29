using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Features.Player;
using Game.Scripts.Features.Player.Model;
using MVVM;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class LaserChargeIndicatorViewModel: IInitializable, IDisposable
    {
        private PlayerModel _playerModel;

        private CancellationTokenSource _cts;

        private bool _isCharging;
        
        private int _maxLaserAttacks;

        [field: Data("CurrentLaserAttacks")]
        private int _currentLaserAttacks;

        private float _chargeTime;

        
        public event Action<float> OnFillAmountChanged;
        public event Action<string> OnLaserAttacksChanged;
        
        public LaserChargeIndicatorViewModel(PlayerModel playerModel)
        {
            _playerModel = playerModel;
            _maxLaserAttacks = _playerModel.MaxLaserAttacks;
            _currentLaserAttacks = _playerModel.CurrentLaserAttacks;
            _chargeTime = _playerModel.LaserChargeTime;
        }
        
        public void Initialize()
        {
            _playerModel.OnLaserAttack += OnCurrentLaserAttackChanged;
            
            LaserAttacksChanged(_currentLaserAttacks);
        }

        public void Dispose()
        {
            _playerModel.OnLaserAttack -= OnCurrentLaserAttackChanged;
        }
        
        private async UniTask StartCharging(float chargeTime)
        {
            if (_cts.IsCancellationRequested)
                return;
            
            _isCharging = true;
            
            float fillAmount;
            
            float elapsed = 0f;

            while (elapsed < chargeTime)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / chargeTime;
                fillAmount = progress;
                
                FillAmountChanged(fillAmount);
                
                await UniTask.Yield(cancellationToken: _cts.Token);
            }
            
            _isCharging = false;
            _currentLaserAttacks++;
            LaserCharge(_currentLaserAttacks);
        }

        private void OnCurrentLaserAttackChanged(int currentLaserAttacks)
        {
            _currentLaserAttacks = currentLaserAttacks;
            
            LaserAttacksChanged(_currentLaserAttacks);

            if (_currentLaserAttacks < _maxLaserAttacks && !_isCharging)
            {
                _cts?.Cancel();
                _cts?.Dispose();
                _cts = new CancellationTokenSource();
                
                _ = StartCharging(_chargeTime);
            }
        }

        private void LaserCharge(int currentLaserAttacks)
        {
            _playerModel.SetLaserCharges(currentLaserAttacks);
            OnCurrentLaserAttackChanged(currentLaserAttacks);
        }

        private void FillAmountChanged(float fillAmount)
        {
            OnFillAmountChanged?.Invoke(fillAmount);
        }

        private void LaserAttacksChanged(int laserAttacks)
        {
            OnLaserAttacksChanged?.Invoke(laserAttacks.ToString());
        }
    }
}