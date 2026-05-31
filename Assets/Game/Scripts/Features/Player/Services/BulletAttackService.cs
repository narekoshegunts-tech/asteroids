using Game.Scripts.Features.Player.Projectiles.Bullets;
using Game.Scripts.Features.Player.Projectiles.Data;
using Zenject;

namespace Game.Scripts.Features.Player.Services
{
    public class BulletAttackService: ProjectileAttackService<Bullet>
    {

        [Inject]
        private void Construct(Bullet bulletPrefab)
        {
            _prefab = bulletPrefab;
        }
        protected override int GetPoolSize(ProjectilesDataService data)
        {
            return data.BulletPoolSize;
        }

        protected override string GetPoolName()
        {
            return "BulletPool";
        }

    }
}