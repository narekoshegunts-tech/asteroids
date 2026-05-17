using System.ComponentModel;
using Game.Scripts.Features.Player;
using Game.Scripts.Features.Player.Projectiles.Data;
using Game.Scripts.Features.Player.Services;
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
            BindProjectilesDataService();
            BindBulletAttackService();
        }

        private void BindBulletAttackService()
        {
            Container
                .Bind<BulletAttackService>()
                .AsSingle();
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

        private void BindProjectilesDataService()
        {
            Container
                .Bind<ProjectilesDataService>()
                .AsSingle()
                .NonLazy();
        }
    }
}