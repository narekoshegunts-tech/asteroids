using Game.Scripts.Common.CustomInput;
using Game.Scripts.Common.CustomInput.Mobile;
using Game.Scripts.Common.CustomInput.MouseKeyboard;
using Game.Scripts.Common.CustomInput.Strategy;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class CustomInputInstallers: MonoInstaller
    {
        [SerializeField] private MobileInput _mobileInputPrefab;
        public override void InstallBindings()
        {
            BindMouseKeyboardInputSystem();
            BindInputStrategy();
            BindCustomInputSystem();
        }

        private void BindInputStrategy()
        {
            if (IsMobilePlatform())
            {
                Container
                    .Bind<MobileInput>()
                    .FromComponentInNewPrefab(_mobileInputPrefab)
                    .AsSingle()
                    .NonLazy();

                Container
                    .Bind<IInputStrategy>()
                    .To<MobileInputStrategy>()
                    .AsSingle();
            }
            else
            {
                Container
                    .Bind<IInputStrategy>()
                    .To<MouseKeyboardInputStrategy>()
                    .AsSingle();
            }
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

        private bool IsMobilePlatform()
        {
            return true;
            return Application.isMobilePlatform 
                   || Application.platform == RuntimePlatform.Android 
                   || Application.platform == RuntimePlatform.IPhonePlayer;
        }
    }
}