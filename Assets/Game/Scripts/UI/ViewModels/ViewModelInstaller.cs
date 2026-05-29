using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class ViewModelInstaller: MonoInstaller
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

            Container
                .BindInterfacesAndSelfTo<PlayerMovementViewModel>()
                .AsSingle()
                .NonLazy();

            Container
                .BindInterfacesAndSelfTo<ScoreViewModel>()
                .AsSingle()
                .NonLazy();
        }
    }
}