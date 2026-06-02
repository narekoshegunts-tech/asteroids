using UnityEngine;

namespace Game.Scripts.Common.CustomInput
{
    public interface INotTargetedInputSystem: IInputSystem
    {
        Vector2 GetDirection();
    }
}