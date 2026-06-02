using System;
using UnityEngine;

namespace Game.Scripts.Common.CustomInput.Mobile
{
    public class MobileInput: MonoBehaviour, INotTargetedInputSystem
    {
        [SerializeField] private VirtualJoystick _joystick;
        [SerializeField] private VirtualButton _accelerationButton;
        [SerializeField] private VirtualButton _bulletButton;
        [SerializeField] private VirtualButton _laserButton;


        public event Action OnAccelerationKeyPressed
        {
            add => _accelerationButton.OnPressed += value;
            remove => _accelerationButton.OnPressed -= value;
        }
        
        public event Action OnAccelerationKeyPressedDown
        {
            add => _accelerationButton.OnPressedDown += value;
            remove => _accelerationButton.OnPressedDown -= value;
        }
        
        public event Action OnAccelerationKeyPressedUp
        {
            add => _accelerationButton.OnPressedUp += value;
            remove => _accelerationButton.OnPressedUp -= value;
        }
        
        public event Action OnBulletAttackKeyPressedDown
        {
            add => _bulletButton.OnPressedDown += value;
            remove => _bulletButton.OnPressedDown -= value;
        }
        
        public event Action OnLaserAttackKeyPressedDown
        {
            add => _laserButton.OnPressedDown += value;
            remove => _laserButton.OnPressedDown -= value;
        }
        
        public Vector2 GetDirection() => _joystick.Direction;
    }
}