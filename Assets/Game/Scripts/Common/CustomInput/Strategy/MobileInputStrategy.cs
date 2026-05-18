using Game.Scripts.Common.CustomInput.Mobile;
using Zenject;

namespace Game.Scripts.Common.CustomInput.Strategy
{
    public class MobileInputStrategy: IInputStrategy
    {
        [Inject] private readonly MobileInput _mobileInput;
        
        public IInputSystem GetInputSystem()
        {
            return _mobileInput;
        }
    }
}