using UnityEngine;
using Game.Scripts.Features.Enemies.Asteroids.Data;

namespace Game.Scripts.Features.Enemies.Asteroids
{
    public class Asteroid: Enemy
    {
        public AsteroidType Type { get; private set; }

        public void Initialize(Vector3 startPosition, AsteroidData data)
        {
            Type = data.Type;

            var scale = Random.Range(data.MinScale, data.MaxScale);
            transform.localScale = scale * Vector3.one;
            
            transform.position = startPosition;
        }
    }
}