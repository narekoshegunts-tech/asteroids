using UnityEngine;

namespace Game.Scripts.Common.CustomInput
{
    public interface ITargetedInputSystem: IInputSystem
    {
        Vector2 GetDirection(Transform targetTransform);
    }
}