using Game.Scripts.Features.Player.Projectiles.Data;
using Zenject;

namespace Game.Scripts.Features.Player.Projectiles.Lasers
{
    public class LaserMovement: ProjectileMovement
    {
        [Inject]
        private void Construct(ProjectilesDataService data)
        {
            var config = data.Data[ProjectileType.Laser];
            _speed = config.MoveSpeed;
        }
    }
}