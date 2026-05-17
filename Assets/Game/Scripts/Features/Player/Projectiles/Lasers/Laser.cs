using Game.Scripts.Features.Enemies;
using Game.Scripts.Features.Player.Projectiles.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player.Projectiles.Lasers
{
    public class Laser: Projectile
    {
        [Inject]
        protected override void Construct(ProjectilesDataService dataService)
        {
            var config = dataService.GetData()[ProjectileType.Laser];
            _lifeTime = config.LifeTime;
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Destroy();
            }
        }
    }
}