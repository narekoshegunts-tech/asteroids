using UnityEngine;

namespace Game.Scripts.CustomPhysics
{
    public class Acceleration2D
    {
        private float _xAcceleration2D = 0;
        private float _yAcceleration2D = 0;
        
        public float XAcceleration2D => _xAcceleration2D;
        public float YAcceleration2D => _yAcceleration2D;

        public void ApplyAcceleration(Vector2 direction, float acceleration)
        {
            _xAcceleration2D = direction.x * acceleration;
            _yAcceleration2D = direction.y * acceleration;
        }

        public void Reset()
        {
            _xAcceleration2D = 0;
            _yAcceleration2D = 0;
        }
        
        
    }
}