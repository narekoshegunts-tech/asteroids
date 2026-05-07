using Game.Scripts.Features.Player;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class PlayerInstallers: MonoInstaller
    {
        [SerializeField] private Player _player;
        [SerializeField] private Transform _startPoint;

        public override void InstallBindings()
        {
            BindPlayer();
        }

        private void BindPlayer()
        {
            Container
                .Bind<Player>()
                .FromComponentInNewPrefab(_player)
                .UnderTransform(_startPoint)
                .AsSingle()
                .NonLazy();
        }
    }
}