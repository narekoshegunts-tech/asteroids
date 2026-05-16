using Game.Scripts.Features.Enemies;
using UnityEngine;


namespace Game.Scripts.Features.Player.Attack
{
    public class Bullet: Projectile
    {

        protected void Awake()
        {
            base.Awake();
            _lifeTime = 1.5f;
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