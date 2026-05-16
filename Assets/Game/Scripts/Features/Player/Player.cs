using Game.Scripts.Features.Enemies;
using UnityEngine;

namespace Game.Scripts.Features.Player
{
    public class Player: MonoBehaviour
    {
        private void GetDamage()
        {
            Debug.Log("GetDamage");
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Enemy>(out var enemy))
            {
                enemy.Destroy();
            }

            GetDamage();
        }
    }
}