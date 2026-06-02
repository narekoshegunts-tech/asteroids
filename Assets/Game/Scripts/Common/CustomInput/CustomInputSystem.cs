using System;
using Game.Scripts.Common.CustomInput.Strategy;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Common.CustomInput
{
    public class CustomInputSystem
    {
        private IInputSystem _currentInput;

        [Inject]
        private void Construct(IInputStrategy inputStrategy)
        {
            _currentInput = inputStrategy.GetInputSystem();
        }
        
        public event Action OnAccelerationKeyPressedDown
        {
            add => _currentInput.OnAccelerationKeyPressedDown += value;
            remove => _currentInput.OnAccelerationKeyPressedDown -= value;
        }
        public event Action OnAccelerationKeyPressed
        {
            add => _currentInput.OnAccelerationKeyPressed += value;
            remove => _currentInput.OnAccelerationKeyPressed -= value;
        }
        public event Action OnAccelerationKeyPressedUp
        {
            add => _currentInput.OnAccelerationKeyPressedUp += value;
            remove => _currentInput.OnAccelerationKeyPressedUp -= value;
        }
        
        public event Action OnBulletAttackKeyPressed
        {
            add => _currentInput.OnBulletAttackKeyPressedDown += value;
            remove => _currentInput.OnBulletAttackKeyPressedDown -= value;
        }
        
        public event Action OnLaserAttackKeyPressed
        {
            add => _currentInput.OnLaserAttackKeyPressedDown += value;
            remove => _currentInput.OnLaserAttackKeyPressedDown -= value;
        }
        public Vector2 GetDirection(Transform targetTransform)
        {
            if(_currentInput is ITargetedInputSystem targetedInputSystem)
                return targetedInputSystem.GetDirection(targetTransform);
            if (_currentInput is INotTargetedInputSystem notTargetedInputSystem)
                return notTargetedInputSystem.GetDirection();

            throw new Exception("Current input doesn't realise correct interface");
        }
    }
}