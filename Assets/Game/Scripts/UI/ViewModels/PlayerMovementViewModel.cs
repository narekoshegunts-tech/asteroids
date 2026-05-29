using System;
using Game.Scripts.Features.Player;
using Game.Scripts.Features.Player.Model;
using MVVM;
using UnityEngine;
using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class PlayerMovementViewModel: IInitializable, IDisposable
    {
        private PlayerMovementModel _playerMovementModel;

        [Data("Position")]
        private string _position;
        [Data("Velocity")]
        private string _velocity;
        [Data("Rotation")]
        private string _rotation;

        public event Action<string> OnPositionChanged;
        public event Action<string> OnVelocityChanged;
        public event Action<string> OnRotationChanged;

        public PlayerMovementViewModel(PlayerMovementModel playerMovementModel)
        {
            _playerMovementModel = playerMovementModel;
        }
        public void Initialize()
        {
            
            _playerMovementModel.OnPositionChanged += ChangePosition;
            _playerMovementModel.OnVelocityChanged += ChangeVelocity;
            _playerMovementModel.OnRotationChanged += ChangeRotation;
        }

        public void Dispose()
        {
            _playerMovementModel.OnPositionChanged -= ChangePosition;
            _playerMovementModel.OnVelocityChanged -= ChangeVelocity;
            _playerMovementModel.OnRotationChanged -= ChangeRotation;
        }
        
        private void ChangePosition(Vector2 position)
        {
            var x = Mathf.Round(position.x);
            var y =  Mathf.Round(position.y);
            _position = $"x: {x}, y: {y}";
            OnPositionChanged?.Invoke(_position);
        }

        private void ChangeVelocity(float velocity)
        {
            _velocity = Mathf.Round(velocity).ToString();
            OnVelocityChanged?.Invoke(_velocity);
        }

        private void ChangeRotation(float rotation)
        {
            _rotation = Mathf.Round(rotation).ToString();
            OnRotationChanged?.Invoke(_rotation);
        }
    }
}