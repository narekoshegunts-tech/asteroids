using Game.Scripts.Common.CustomInput;
using Zenject;

namespace Game.Scripts.Installers
{
    public class CustomInputInstallers: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindMouseKeyboardInputSystem();
            BindCustomInputSystem();
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