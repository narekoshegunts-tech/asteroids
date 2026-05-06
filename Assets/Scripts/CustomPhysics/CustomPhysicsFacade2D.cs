using UnityEngine;
using Zenject;

namespace CustomPhysics
{
    public class CustomPhysicsFacade2D
    {
        private Acceleration2D _acceleration2D;
        private Velocity2D _velocity2D;
        private Position2D _position2D;

        private Transform _transform;

        [Inject]
        private void Construct(Acceleration2D acceleration2D, Velocity2D.Factory velocity2DFactory, Position2D.Facory position2DFactory)
        {
            _acceleration2D = acceleration2D;
            _velocity2D = velocity2DFactory.Create(_acceleration2D);
            _position2D = position2DFactory.Create(_transform, _velocity2D);
        }

        public CustomPhysicsFacade2D(Transform transform)
        {
            _transform = transform;
        }
        
        public void Update(float deltaTime)
        {
            _velocity2D.UpdateVelocity(deltaTime);
            _position2D.Update(deltaTime);
        }
        
        public void ApplyAcceleration(Vector2 acceleration)
        {
            _acceleration2D.ApplyAcceleration(acceleration);
        }
        
        public class Factory : PlaceholderFactory<Transform,CustomPhysicsFacade2D>
        {
        }
    }
}