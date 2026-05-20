using UnityEngine;
using Zenject;

namespace Game.Scripts.CustomPhysics
{
    public class Rotation2D
    {
        private Transform _transform;

        private const float AngleOffset = -90; // у нас спрайты смотрят вверх изначально
        
        private float _rotation;

        public Vector2 Direction { get; private set; }
        public float Rotation => _rotation;

        public Rotation2D(Transform transform)
        {
            _transform = transform;
        }

        public void ApplyRotation(Vector2 direction)
        {
            Direction = direction;
            
            _rotation = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            _rotation += AngleOffset;
            
            _transform.eulerAngles = new Vector3(0, 0, _rotation);
        }
    }
}