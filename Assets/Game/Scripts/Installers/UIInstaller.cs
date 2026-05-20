
using Game.Scripts.UI.Game;
using Zenject;

namespace Game.Scripts.UI
{
    public class UIInstallers: MonoInstaller
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