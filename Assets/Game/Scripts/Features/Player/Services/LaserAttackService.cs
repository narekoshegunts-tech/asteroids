using Game.Scripts.Features.Player.Projectiles.Data;
using Game.Scripts.Features.Player.Projectiles.Lasers;

namespace Game.Scripts.Features.Player.Services
{
    public class LaserAttackService: ProjectileAttackService<Laser>
    {
        protected override int GetPoolSize(ProjectilesDataService data)
        {
            return data.LaserPoolSize;
        }

        protected override string GetPoolName()
        {
            return "LaserPool";
        }

        protected override string GetPrefabPath()
        {
            return "Prefabs/Projectiles/Laser";
        }
    }
}