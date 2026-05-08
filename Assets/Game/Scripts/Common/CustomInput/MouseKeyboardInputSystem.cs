using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Common.CustomInput
{
    public class MouseKeyboardInputSystem: ITickable
    {
        [Inject] private Camera _camera;
        private Transform _targetTransform;

        private KeyCode _accelerationKey = KeyCode.W;
        private KeyCode _bulletAttack = KeyCode.Mouse0;

        public bool IsAccelerationKeyPressed => Input.GetKey(_accelerationKey);

        public event Action OnAccelerationKeyPressed;
        public event Action OnBulletAttackKeyPressed;

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


        public void Tick()
        {
            if (Input.GetKey(_accelerationKey))
            {
                OnAccelerationKeyPressed?.Invoke();
            }

            if (Input.GetKeyDown(_bulletAttack))
            {
                OnBulletAttackKeyPressed?.Invoke();
            }
        }
    }
}