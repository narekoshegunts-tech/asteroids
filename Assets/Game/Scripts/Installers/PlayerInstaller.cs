using Game.Scripts.Features.Player;
using Game.Scripts.Features.Player.Data;
using Game.Scripts.Features.Player.Interfaces;
using Game.Scripts.Features.Player.Model;
using Game.Scripts.Features.Player.Projectiles.Bullets;
using Game.Scripts.Features.Player.Projectiles.Data;
using Game.Scripts.Features.Player.Projectiles.Lasers;
using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class PlayerInstaller: MonoInstaller
    {
        [SerializeField] private Player _playerPrefab;
        [SerializeField] private Transform _startPoint;
        
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Laser _laserPrefab; 

        public override void InstallBindings()
        {
            BindProjectilesPrefabs();
            BindPlayer();
            BindProjectilesDataService();
            BindBulletAttackService();
            BindLaserAttackService();
            BindPlayerDataService();
            BindPlayerModel();
            BindPlayerStateService();
            BindPlayerLifeService();
            BindPlayerMovementService();
        }

        private void BindProjectilesPrefabs()
        {
            Container
                .Bind<Bullet>()
                .FromInstance(_bulletPrefab);

            Container
                .Bind<Laser>()
                .FromInstance(_laserPrefab);
        }

        private void BindPlayerMovementService()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerMovementService>()
                .AsSingle();
        }
        private void BindPlayerLifeService()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerLifeService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerStateService()
        {
            Container
                .Bind<PlayerStateService>()
                .AsSingle();
        }

        private void BindPlayerModel()
        {
            Container
                .Bind<PlayerHealthModel>()
                .AsSingle();
            Container
                .Bind<PlayerMovementModel>()
                .AsSingle();
            Container
                .Bind<PlayerLaserAttackModel>()
                .AsSingle();
        }

        private void BindLaserAttackService()
        {
            Container
                .Bind<LaserAttackService>()
                .AsSingle();
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
                .Bind(typeof(Player),
                        typeof(PlayerMovement),
                        typeof(IPlayerTransform),
                        typeof(IPlayerDirection))
                .FromComponentInNewPrefab(_playerPrefab)
                .UnderTransform(_startPoint)
                .AsSingle();
            

        }

        private void BindProjectilesDataService()
        {
            Container
                .Bind<ProjectilesDataService>()
                .AsSingle();
        }
        
        private void BindPlayerDataService()
        {
            Container
                .Bind<PlayerDataService>()
                .AsSingle();
        }
    }
}