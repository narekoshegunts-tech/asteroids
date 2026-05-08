using Game.Scripts.CustomPhysics.Factories;
using UnityEngine;
using Zenject;

namespace Game.Scripts.CustomPhysics
{
    public class CustomPhysicsFacade2D
    {
        private Acceleration2D _acceleration2D;
        private Velocity2D _velocity2D;
        private Position2D _position2D;
        private Rotation2D _rotation2D;

        private Transform _transform;
        
        public Vector2 Direction => _rotation2D.Direction;

        [Inject]
        private void Construct(Acceleration2D acceleration2D, Velocity2DFactory velocity2DFactory, 
            Position2DFactory position2DFactory, Rotation2DFactory rotation2DFactory)
        {
            _acceleration2D = acceleration2D;
            _velocity2D = velocity2DFactory.Create(_acceleration2D);
            _position2D = position2DFactory.Create(_transform, _velocity2D);
            _rotation2D = rotation2DFactory.Create(_transform);
        }

        public CustomPhysicsFacade2D(Transform transform)
        {
            _transform = transform;
        }
        public void Update()
        {
            _velocity2D.UpdateVelocity(Time.deltaTime);
            _position2D.Update(Time.deltaTime);
            _acceleration2D.Reset();
        }
        
        public void ApplyAcceleration(float acceleration)
        {
            _acceleration2D.ApplyAcceleration(acceleration, _rotation2D);
        }

        public void ApplyVelocity(float velocity)
        {
            _velocity2D.ApplyVelocity(velocity, _rotation2D);
        }

        public void ApplyRotation(Vector2 direction)
        {
            _rotation2D.ApplyRotation(direction);
        }

        public void ApplyPosition(Vector3 position)
        {
            _position2D.ApplyPosition(position);
        }

        
    }
}