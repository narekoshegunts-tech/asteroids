using Zenject;

namespace Game.Scripts.Features.UI.ViewModels
{
    public class ViewModelsInstaller: MonoInstaller
    {

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<HealthViewModel>()
                .AsSingle()
                .NonLazy();
        }
    }
}