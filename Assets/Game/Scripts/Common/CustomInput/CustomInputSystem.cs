using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Common.CustomInput
{
    public class CustomInputSystem: IInitializable, IDisposable
    {
        [Inject] MouseKeyboardInputSystem _mouseKeyboardInputSystem;

        private Transform _targetTransform;

        public event Action OnAccelerationKeyPressed;
        public void SetTargetTransform(Transform targetTransform)
        {
            _targetTransform = targetTransform;
            _mouseKeyboardInputSystem.SetTargetTransform(_targetTransform);
        }
        public Vector2 GetDirection()
        {
            return _mouseKeyboardInputSystem.GetDirection();
        }

        private void OnInputAccelerationKeyPressed()
        {
            OnAccelerationKeyPressed?.Invoke();
        }

        public void Dispose()
        {
            _mouseKeyboardInputSystem.OnAccelerationKeyPressed -= OnInputAccelerationKeyPressed;
        }

        public void Initialize()
        {
            _mouseKeyboardInputSystem.OnAccelerationKeyPressed += OnInputAccelerationKeyPressed;
        }
    }
}