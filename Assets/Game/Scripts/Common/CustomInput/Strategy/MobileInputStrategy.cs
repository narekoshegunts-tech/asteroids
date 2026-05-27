using Game.Scripts.Common.CustomInput.Mobile;

namespace Game.Scripts.Common.CustomInput.Strategy
{
    public class MobileInputStrategy: IInputStrategy
    {
        private MobileInput _mobileInput;

        private void Construct(MobileInput mobileInput)
        {
            _mobileInput = mobileInput;
        }
        
        public IInputSystem GetInputSystem()
        {
            return _mobileInput;
        }
    }
}