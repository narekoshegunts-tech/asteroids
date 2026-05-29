using System;
using Game.Scripts.CustomPhysics;
using Game.Scripts.Features.Player.Model;
using UnityEngine;

namespace Game.Scripts.Features.Player.Services
{
    public class PlayerMovementService: IDisposable
    {
        private PlayerMovementModel _playerMovementModel;
        
        private CustomPhysicsFacade2D _playerPhysics;

        public PlayerMovementService(PlayerMovementModel playerMovementModel)
        {
            _playerMovementModel = playerMovementModel;
        }

        public void Initialize(CustomPhysicsFacade2D playerPhysics)
        {
            _playerPhysics = playerPhysics;

            _playerPhysics.OnPositionChanged += ChangePosition;
            _playerPhysics.OnRotationChanged += ChangeRotation;
            _playerPhysics.OnVelocityChanged += ChangeVelocity;
        }

        private void ChangePosition(Vector2 position)
        {
            _playerMovementModel.ChangePosition(position);
        }

        private void ChangeVelocity(float instantVelocity)
        {
            _playerMovementModel.ChangeVelocity(instantVelocity);
        }

        private void ChangeRotation(float instantRotation)
        {
            _playerMovementModel.ChangeRotation(instantRotation);
        }

        public void Dispose()
        {
            _playerPhysics.OnPositionChanged -= ChangePosition;
            _playerPhysics.OnRotationChanged -= ChangeRotation;
            _playerPhysics.OnVelocityChanged -= ChangeVelocity;
        }
    }
}