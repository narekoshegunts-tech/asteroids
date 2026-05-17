using Game.Scripts.Features.Player.Projectiles.Bullets;
using Game.Scripts.Features.Player.Projectiles.Data;

namespace Game.Scripts.Features.Player.Services
{
    public class BulletAttackService: ProjectileAttackService<Bullet>
    {
        protected override int GetPoolSize(ProjectilesDataService data)
        {
            return data.BulletPoolSize;
        }

        protected override string GetPoolName()
        {
            return "BulletPool";
        }

        protected override string GetPrefabPath()
        {
            return "Prefabs/Projectiles/Bullet";
        }

    }
}