using UnityEngine;
using Zenject;

namespace Game.Scripts.Common.CustomInput
{
    public class CustomInputSystem
    {
        [Inject] MouseKeyboardInputSystem _mouseKeyboardInputSystem;

        private Transform _targetTransform; 

        public void SetTargetTransform(Transform targetTransform)
        {
            _targetTransform = targetTransform;
            _mouseKeyboardInputSystem.SetTargetTransform(_targetTransform);
        }
        public Vector2 GetDirection()
        {
            return _mouseKeyboardInputSystem.GetDirection();
        }

        public bool IsAccelerationKeyPressed()
        {
            return _mouseKeyboardInputSystem.IsAccelerationKeyPressed;
        }
    }
}