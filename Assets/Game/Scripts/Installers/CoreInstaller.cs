using Game.Scripts.Features.Core.Score;
using Game.Scripts.Features.Core.Score.Data;
using Zenject;

namespace Game.Scripts.Installers
{
    public class CoreInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ScoreDataService>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<ScoreService>()
                .AsSingle();
        }
    }
}