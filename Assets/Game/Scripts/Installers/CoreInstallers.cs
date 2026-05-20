using Game.Scripts.Features.Core.Score.Data;
using Zenject;

namespace Game.Scripts.Installers
{
    public class CoreInstallers: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ScoreDataService>()
                .AsSingle();
        }
    }
}