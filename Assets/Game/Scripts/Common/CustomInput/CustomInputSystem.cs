using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Common.CustomInput
{
    public class CustomInputSystem
    {
        [Inject] MouseKeyboardInputSystem _mouseKeyboardInputSystem;

        private Transform _targetTransform;

        public event Action OnAccelerationKeyPressed
        {
            add => _mouseKeyboardInputSystem.OnAccelerationKeyPressed += value;
            remove => _mouseKeyboardInputSystem.OnAccelerationKeyPressed -= value;
        }
        
        public event Action OnBulletAttackKeyPressed
        {
            add => _mouseKeyboardInputSystem.OnBulletAttackKeyPressed += value;
            remove => _mouseKeyboardInputSystem.OnBulletAttackKeyPressed -= value;
        }
        
        // не верю что из за этой хуйни надо будет делать фабрику... Я другого решения не нашел
        public void SetTargetTransform(Transform targetTransform)
        {
            _targetTransform = targetTransform;
            _mouseKeyboardInputSystem.SetTargetTransform(_targetTransform);
        }
        public Vector2 GetDirection()
        {
            return _mouseKeyboardInputSystem.GetDirection();
        }
    }
}