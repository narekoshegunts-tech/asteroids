using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Common.CustomInput.MouseKeyboard
{
    public class MouseKeyboardInputSystem: IInputSystem, ITickable
    {
        [Inject] private Camera _camera;
        private Transform _targetTransform;

        private KeyCode _accelerationKey = KeyCode.W;
        private KeyCode _bulletAttack = KeyCode.Mouse0;
        private KeyCode _laserAttack  = KeyCode.Mouse1;
        
        public event Action OnAccelerationKeyPressed;
        public event Action OnAccelerationKeyPressedDown;
        public event Action OnAccelerationKeyPressedUp;
        public event Action OnBulletAttackKeyPressedDown;
        public event Action OnLaserAttackKeyPressedDown;

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

            if (Input.GetKeyDown(_accelerationKey))
            {
                OnAccelerationKeyPressedDown?.Invoke();
            }
            if (Input.GetKeyUp(_accelerationKey))
            {
                OnAccelerationKeyPressedUp?.Invoke();
            }

            if (Input.GetKeyDown(_bulletAttack))
            {
                OnBulletAttackKeyPressedDown?.Invoke();
            }

            if (Input.GetKeyDown(_laserAttack))
            {
                OnLaserAttackKeyPressedDown?.Invoke();
            }
        }
    }
}