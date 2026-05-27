namespace Game.Scripts.CustomPhysics
{
    public class Acceleration2D
    {
        private float _xAcceleration2D = 0;
        private float _yAcceleration2D = 0;
        
        public float XAcceleration2D => _xAcceleration2D;
        public float YAcceleration2D => _yAcceleration2D;

        public void ApplyAcceleration(float acceleration, Rotation2D rotation)
        {
            _xAcceleration2D = rotation.Direction.x * acceleration;
            _yAcceleration2D = rotation.Direction.y * acceleration;
        }
    }
}