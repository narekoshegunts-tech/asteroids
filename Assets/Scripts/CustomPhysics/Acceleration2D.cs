using UnityEngine;

namespace CustomPhysics
{
    public class Acceleration2D
    {
        private float _xAcceleration2D = 0;
        private float _yAcceleration2D = 0;
        
        public float XAcceleration2D => _xAcceleration2D;
        public float YAcceleration2D => _yAcceleration2D;

        public void ApplyAcceleration(Vector2 acceleration)
        {
            _xAcceleration2D = acceleration.x;
            _yAcceleration2D = acceleration.y;
        }
    }
}