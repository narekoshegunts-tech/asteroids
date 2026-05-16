using Game.Scripts.Features.Enemies;
using UnityEngine;


namespace Game.Scripts.Features.Player.Attack
{
    public class Bullet: Projectile
    {

        private void Awake()
        {
            _lifeTime = 3.5f;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Enemy enemy))
            {
                enemy.Destroy();
            }
            Destroy();
        }
    }
}