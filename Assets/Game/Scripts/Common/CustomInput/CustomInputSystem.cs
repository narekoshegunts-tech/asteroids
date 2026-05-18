using System;
using Game.Scripts.Common.CustomInput.Mobile;
using Game.Scripts.Common.CustomInput.MouseKeyboard;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Common.CustomInput
{
    public class CustomInputSystem
    {
        private MouseKeyboardInputSystem _mouseKeyboardInputSystem;
        private MobileInput _mobileInput;
        
        private IInputSystem _currentInput;

        private Transform _targetTransform;

        [Inject]
        private void Construct(MouseKeyboardInputSystem mouseKeyboardInputSystem, MobileInput mobileInput)
        {
            _mouseKeyboardInputSystem = mouseKeyboardInputSystem;
            _mobileInput = mobileInput;
            _currentInput = _mobileInput;
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
        
        // не верю что из за этой хуйни надо будет делать фабрику... Я другого решения не нашел
        public void SetTargetTransform(Transform targetTransform)
        {
            _targetTransform = targetTransform;
            _mouseKeyboardInputSystem.SetTargetTransform(_targetTransform);
        }
        public Vector2 GetDirection()
        {
            return _currentInput.GetDirection();
        }
    }
}