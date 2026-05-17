using Game.Scripts.Features.Player.Projectiles.Data;
using Zenject;

namespace Game.Scripts.Features.Player.Projectiles.Bullets
{
    public class BulletMovement: ProjectileMovement
    {

        [Inject]
        private void Construct(ProjectilesDataService data)
        {
            var config = data.GetData()[ProjectileType.Bullet];
            _speed = config.MoveSpeed;
        }
    }
}