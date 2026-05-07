using UnityEngine;
using Zenject;

namespace Game.Scripts.CustomPhysics
{
    public class Rotation2D
    {
        private Transform _transform;

        private const float AngleOffset = -90; // у нас спрайты смотрят вверх изначально
        
        public Vector2 Direction { get; private set; }

        public Rotation2D(Transform transform)
        {
            _transform = transform;
        }

        public void ApplyRotation(Vector2 direction)
        {
            Direction = direction;
            
            float rotationZ = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            rotationZ += AngleOffset;
            
            _transform.eulerAngles = new Vector3(0, 0, rotationZ);
        }
    }
}