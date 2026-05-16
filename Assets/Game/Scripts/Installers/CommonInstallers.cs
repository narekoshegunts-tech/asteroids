using Game.Scripts.Common.ObjectPool;
using Zenject;

namespace Game.Scripts.Installers
{
    public class CommonInstallers: MonoInstaller
    {
        public override void InstallBindings()
        {
            BindObjectPoolFactory();
        }

        private void BindObjectPoolFactory()
        {
            Container
                .Bind<ObjectPoolFactory>()
                .AsSingle()
                .NonLazy();
        }
    }
}