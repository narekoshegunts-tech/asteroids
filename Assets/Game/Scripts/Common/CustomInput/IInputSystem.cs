using System;

namespace Game.Scripts.Common.CustomInput
{
    public interface IInputSystem
    {
        public event Action OnAccelerationKeyPressed;
        public event Action OnAccelerationKeyPressedDown;
        public event Action OnAccelerationKeyPressedUp;
        public event Action OnBulletAttackKeyPressedDown;
        public event Action OnLaserAttackKeyPressedDown;
    }
}