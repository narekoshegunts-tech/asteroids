using Game.Scripts.Common.CustomInput.MouseKeyboard;
using Zenject;

namespace Game.Scripts.Common.CustomInput.Strategy
{
    public class MouseKeyboardInputStrategy: IInputStrategy
    {
        private MouseKeyboardInputSystem _mouseKeyboardInput;

        [Inject]
        private void Construct(MouseKeyboardInputSystem mouseKeyboardInputSystem)
        {
            _mouseKeyboardInput = mouseKeyboardInputSystem;
        }
        public IInputSystem GetInputSystem()
        {
            return _mouseKeyboardInput;
        }
    }
}