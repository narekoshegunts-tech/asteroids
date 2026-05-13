using Game.Scripts.Common;
using Game.Scripts.Common.ObjectPool;
using Zenject;

namespace Game.Scripts.Installers
{
    public class CommonInstallers: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindObjectPoolFactory();
            BindCameraService();
        }

        private void BindObjectPoolFactory()
        {
            Container
                .Bind<ObjectPoolFactory>()
                .AsSingle()
                .NonLazy();
        }

        private void BindCameraService()
        {
            Container
                .Bind<CameraService>()
                .AsSingle()
                .NonLazy();
        }
    }
}