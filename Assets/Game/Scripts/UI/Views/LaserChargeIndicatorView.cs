using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MVVM;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Game.Scripts.UI.Views
{
    public class LaserChargeIndicatorView: MonoBehaviour
    {
        [SerializeField] private Image _fillImage;
        [SerializeField] private TMP_Text _currentLaserAttacks;
        
        private int _currentCharges;
        private int _maxCharges;
        private float _chargeTime;

        private bool _isCharging;
        
        private CancellationTokenSource _cts = new();

        public event Action<int> OnChargeComplete;
        
        public void Initialize(int maxLaserAttacks, float chargeTime)
        {
            _maxCharges = maxLaserAttacks;
            _currentCharges = maxLaserAttacks;
            _chargeTime = chargeTime;
            
            UpdateVisuals();
        }

        [Method("CurrentLaserAttacks")]
        public void UpdateLaserAttacks(int currentCharges)
        {
            _currentCharges = currentCharges;
            UpdateVisuals();

            if (_currentCharges < _maxCharges && !_isCharging)
            {
                _isCharging = true;
                StartCharging(_chargeTime).Forget();
            }
        }
        
        private async UniTaskVoid StartCharging(float chargeTime)
        {
            _fillImage.fillAmount = 0f;
            
            float elapsed = 0f;

            while (elapsed < chargeTime)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / chargeTime;
                _fillImage.fillAmount = progress;
                await UniTask.Yield(cancellationToken: _cts.Token);
            }
            _isCharging = false;
            _currentCharges++;
            
            OnChargeComplete?.Invoke(_currentCharges);
            
            UpdateLaserAttacks(_currentCharges);
        }

        private void UpdateVisuals()
        {
            _currentLaserAttacks.text = $"{_currentCharges}";
            _fillImage.fillAmount = _currentCharges >= _maxCharges ? 1f : 0f;
        }
    }
}