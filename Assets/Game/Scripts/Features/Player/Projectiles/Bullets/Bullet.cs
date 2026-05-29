using Game.Scripts.Features.Enemies;
using Game.Scripts.Features.Player.Projectiles.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player.Projectiles.Bullets
{
    public class Bullet: Projectile
    {

        [Inject]
        protected override void Construct(ProjectilesDataService dataService)
        {
            var config = dataService.Data[ProjectileType.Bullet];
            _lifeTime = config.LifeTime;
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Die();
                Destroy();
            }
        }
    }
}