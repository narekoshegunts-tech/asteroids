using System.Collections.Generic;

namespace Game.Scripts.Features.Enemies.Asteroids.Data
{
    [System.Serializable]
    public class AsteroidDataRoot
    {
        public int PoolSize;
        public List<AsteroidData> Asteroids;
    }
}