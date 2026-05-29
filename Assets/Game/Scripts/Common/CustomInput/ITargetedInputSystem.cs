using UnityEngine;

namespace Game.Scripts.Common.CustomInput
{
    public interface ITargetedInputSystem: IInputSystem
    {
        void SetTargetTransform(Transform targetTransform);
    }
}