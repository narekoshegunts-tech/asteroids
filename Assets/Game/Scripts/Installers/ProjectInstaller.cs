using Game.Scripts.SDK;
using Zenject;

namespace Game.Scripts.Installers
{
    public class ProjectInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<YandexBannerService>()
                .AsSingle()
                .NonLazy();
        }
    }
}