using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class ViewModelsInstaller: MonoInstaller
    {

        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<HealthViewModel>()
                .AsSingle()
                .NonLazy();
            
            Container
                .BindInterfacesAndSelfTo<LaserChargeIndicatorViewModel>()
                .AsSingle()
                .NonLazy();
        }
    }
}