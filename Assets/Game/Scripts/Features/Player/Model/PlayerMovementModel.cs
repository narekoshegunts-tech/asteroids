using System;
using Game.Scripts.Features.Player.Data;
using UnityEngine;

namespace Game.Scripts.Features.Player.Model
{
    public class PlayerMovementModel
    {
        public event Action<Vector2> OnPositionChanged;
        public event Action<float> OnRotationChanged;
        public event Action <float> OnVelocityChanged;
        
        public Vector2 Position { get; private set; }
        public float Rotation { get; private set; }
        public float Velocity { get; private set; }
        
        public PlayerMovementModel(PlayerDataService playerDataService)
        {
            Position = Vector2.zero;
            Rotation = 0;
            Velocity = 0;
        }
        
        public void ChangePosition(Vector2 position)
        {
            Position = position;
            OnPositionChanged?.Invoke(position);
        }

        public void ChangeRotation(float rotation)
        {
            Rotation = rotation;
            OnRotationChanged?.Invoke(rotation);
        }

        public void ChangeVelocity(float velocity)
        {
            Velocity = velocity;
            OnVelocityChanged?.Invoke(velocity);
        }
    }
}