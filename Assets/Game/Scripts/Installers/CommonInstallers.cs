using Game.Scripts.Common.ObjectPool;
using Game.Scripts.Signals;
using Game.Scripts.UI.Game.Services;
using Zenject;

namespace Game.Scripts.Installers
{
    public class CommonInstallers: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindObjectPoolFactory();
            InstallSignalBus();
            DeclareSignals();
            BindPauseService();
            BindGameFlowController();
        }

        private void BindObjectPoolFactory()
        {
            Container
                .Bind<ObjectPoolFactory>()
                .AsSingle()
                .NonLazy();
        }
        
        public void DeclareSignals()
        {
            Container.DeclareSignal<PlayerDiedSignal>();
            Container.DeclareSignal<RestartRequestSignal>();
        }

        private void InstallSignalBus()
        {
            SignalBusInstaller.Install(Container);
        }

        private void BindPauseService()
        {
            Container
                .Bind<GamePauseService>()
                .AsSingle();
        }

        private void BindGameFlowController()
        {
            Container
                .BindInterfacesAndSelfTo<GameFlowController>()
                .AsSingle()
                .NonLazy();
        }
    }
}