using Game.Scripts.UI.Game;
using Zenject;

namespace Game.Scripts.Installers
{
    public class UIInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEndGameUI();
        }

        private void BindEndGameUI()
        {
            Container
                .Bind<EndGame>()
                .FromComponentInHierarchy()
                .AsSingle();
        }
    }
}