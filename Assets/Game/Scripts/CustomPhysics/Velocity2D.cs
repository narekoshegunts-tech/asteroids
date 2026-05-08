using UnityEngine;
using Zenject;

namespace Game.Scripts.CustomPhysics
{
    public class Velocity2D
    {
        private float _velocityX;
        private float _velocityY;
        
        public float  VelocityX => _velocityX;
        public float  VelocityY => _velocityY;
        
        private Acceleration2D _acceleration2D;

        public Velocity2D(Acceleration2D acceleration2D)
        {
            _acceleration2D = acceleration2D;

            _velocityX = 0;
            _velocityY = 0;
        }

        public void UpdateVelocity(float deltaTime)
        {
            _velocityX += _acceleration2D.XAcceleration2D * deltaTime;
            _velocityY += _acceleration2D.YAcceleration2D * deltaTime;
        }

        public void ApplyVelocity(float velocity, Rotation2D rotation)
        {
            _velocityX = velocity * rotation.Direction.x;
            _velocityY = velocity * rotation.Direction.y;
        }
        
    }
}