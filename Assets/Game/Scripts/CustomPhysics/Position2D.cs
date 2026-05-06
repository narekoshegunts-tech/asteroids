using UnityEngine;
using Zenject;

namespace Game.Scripts.CustomPhysics
{
    public class Position2D
    {
        private float _x;
        private float _y;
        
        private Transform _transform;
        
        private Velocity2D _velocity2D;

        public float X => _x;
        public float Y => _y;

        public Position2D(Transform transform, Velocity2D velocity2D)
        {
            _transform = transform;
            _velocity2D = velocity2D;
            
            _x = _transform.position.x;
            _y = _transform.position.y;
        }

        public void Update(float deltaTime)
        {
            _x += _velocity2D.VelocityX  * deltaTime;
            _y += _velocity2D.VelocityY * deltaTime;
            
            _transform.position = new Vector2(_x, _y);
        }
    }
}
