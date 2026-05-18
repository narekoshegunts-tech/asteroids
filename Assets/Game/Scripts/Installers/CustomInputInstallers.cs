using Game.Scripts.Common.CustomInput;
using Game.Scripts.Common.CustomInput.Mobile;
using Game.Scripts.Common.CustomInput.MouseKeyboard;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class CustomInputInstallers: MonoInstaller
    {
        [SerializeField] private MobileInput _mobileInput;
        public override void InstallBindings()
        {
            BindMouseKeyboardInputSystem();
            BindMobileInputSystem();
            BindCustomInputSystem();
        }

        private void BindMobileInputSystem()
        {
            Container
                .Bind<MobileInput>()
                .FromInstance(_mobileInput)
                .AsSingle()
                .NonLazy();
        }

        private void BindMouseKeyboardInputSystem()
        {
            Container
                .BindInterfacesAndSelfTo<MouseKeyboardInputSystem>()
                .AsSingle()
                .NonLazy();
        }

        private void BindCustomInputSystem()
        {
            Container
                .Bind<CustomInputSystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}