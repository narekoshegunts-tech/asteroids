using MVVM;
using Zenject;

namespace Game.Scripts.UI.Binders
{
    public class BindersInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            BinderFactory.RegisterBinder<HealthBinder>();
            BinderFactory.RegisterBinder<LaserChargeIndicatorBinder>();
            BinderFactory.RegisterBinder<PlayerMovementBinder>();
            BinderFactory.RegisterBinder<ScoreBinder>();
        }
    }
}