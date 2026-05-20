namespace Game.Scripts.Features.Enemies.Asteroids.Data
{
    [System.Serializable]
    public struct AsteroidData
    {
        public EnemyType EnemyType;
        public AsteroidType Type;
        public float MinScale;
        public float MaxScale;
        public float MinSpeed;
        public float MaxSpeed;
        public float Mass;
    }
}