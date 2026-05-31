using Game.Scripts.Features.Player.Projectiles.Data;
using Game.Scripts.Features.Player.Projectiles.Lasers;
using Zenject;

namespace Game.Scripts.Features.Player.Services
{
    public class LaserAttackService: ProjectileAttackService<Laser>
    {

        [Inject]
        private void Construct(Laser laserPrefab)
        {
            _prefab = laserPrefab;
        }
        protected override int GetPoolSize(ProjectilesDataService data)
        {
            return data.LaserPoolSize;
        }

        protected override string GetPoolName()
        {
            return "LaserPool";
        }
    }
}