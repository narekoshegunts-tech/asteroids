using Game.Scripts.CustomPhysics.Factories;
using UnityEngine;
using Zenject;

namespace Game.Scripts.CustomPhysics
{
    public class CustomPhysicsFacade2D
    {
        [Inject] private SimulateCollisionService _simulateCollisionService;
        
        private Acceleration2D _acceleration2D;
        private Velocity2D _velocity2D;
        private Position2D _position2D;
        private Rotation2D _rotation2D;

        private Transform _transform;
        
        public Vector2 Direction => _rotation2D.Direction;

        public float Mass;

        [Inject]
        private void Construct(Acceleration2D acceleration2D, Velocity2DFactory velocity2DFactory, 
            Position2DFactory position2DFactory, Rotation2DFactory rotation2DFactory)
        {
            _acceleration2D = acceleration2D;
            _velocity2D = velocity2DFactory.Create(_acceleration2D);
            _position2D = position2DFactory.Create(_transform, _velocity2D);
            _rotation2D = rotation2DFactory.Create(_transform);
        }

        public CustomPhysicsFacade2D(Transform transform, float mass)
        {
            _transform = transform;
            Mass = mass;
        }

        public void FixedUpdate()
        {
            _velocity2D.UpdateVelocity(Time.fixedDeltaTime);
            _position2D.Update(Time.fixedDeltaTime);
        }
        
        public void ApplyAcceleration(float acceleration)
        {
            _acceleration2D.ApplyAcceleration(acceleration, _rotation2D);
        }

        public void ApplyVelocity(float velocity)
        {
            _velocity2D.ApplyVelocity(velocity, _rotation2D);
        }

        public void ApplyVelocity(Vector2 velocity)
        {
            _velocity2D.ApplyVelocity(velocity);
        }

        public void ApplyRotation(Vector2 direction)
        {
            _rotation2D.ApplyRotation(direction);
        }

        public void ApplyPosition(Vector2 position)
        {
            _position2D.ApplyPosition(position);
        }

        public Vector2 GetPosition()
        {
            return new Vector2(_position2D.X, _position2D.Y);
        }

        public void MoveTo(Vector3 position, float speed)
        {
            var direction = (position - _transform.position).normalized;
            _velocity2D.ApplyVelocity(direction * speed);
        }

        public void SetRandomDirectionToMove(float speed)
        {
            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            _velocity2D.ApplyVelocity(direction * speed);
        }

        public Vector2 GetVelocity()
        {
            return new Vector2(_velocity2D.VelocityX, _velocity2D.VelocityY);
        }

        public void Collision(CustomPhysicsFacade2D other)
        {
            _simulateCollisionService.Simulate(this, other, out var velocity1, out var velocity2);
            ApplyVelocity(velocity1);
            other.ApplyVelocity(velocity2);
        }
    }
}