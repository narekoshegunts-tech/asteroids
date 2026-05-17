using System.Collections.Generic;

namespace Game.Scripts.Features.Player.Projectiles.Data
{
    public class ProjectileDataRoot
    {
        public int BulletPoolSize;
        public int LaserPoolSize;

        public List<ProjectileData> Projectiles;
    }
}