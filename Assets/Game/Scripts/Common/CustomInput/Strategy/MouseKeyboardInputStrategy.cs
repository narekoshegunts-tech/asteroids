using Game.Scripts.Common.CustomInput.MouseKeyboard;
using Zenject;

namespace Game.Scripts.Common.CustomInput.Strategy
{
    public class MouseKeyboardInputStrategy: IInputStrategy
    {
        [Inject] private MouseKeyboardInputSystem _mouseKeyboardInput;
        
        public IInputSystem GetInputSystem()
        {
            return _mouseKeyboardInput;
        }
    }
}