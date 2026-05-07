using UnityEngine;
using Zenject;

namespace Game.Scripts.Common.CustomInput
{
    public class MouseKeyboardInputSystem
    {
        [Inject] private Camera _camera;
        private Transform _targetTransform;

        private KeyCode _accelerationKey = KeyCode.W;

        public bool IsAccelerationKeyPressed => Input.GetKey(_accelerationKey);

        public void SetTargetTransform(Transform targetTransform)
        {
            _targetTransform = targetTransform;
        }
        
        public Vector2 GetDirection()
        {
            Vector2 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction =  mousePosition - new Vector2(_targetTransform.position.x, _targetTransform.position.y);
            
            return direction.normalized;
        }
        
        
    }
}