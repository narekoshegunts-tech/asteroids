using Game.Scripts.Features.Enemies;
using Game.Scripts.Features.Player.Projectiles.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player.Projectiles.Bullets
{
    public class Bullet: Projectile
    {

        [Inject]
        private void Construct(ProjectilesDataService data)
        {
            var config = data.GetData()[ProjectileType.Bullet];
            _lifeTime = config.LifeTime;
        }

        protected new void Awake()
        {
            base.Awake();
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Enemy>(out Enemy enemy))
            {
                enemy.Destroy();
                Destroy();
            }
        }
    }
}