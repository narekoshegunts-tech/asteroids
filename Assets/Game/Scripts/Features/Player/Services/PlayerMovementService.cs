using System;
using Game.Scripts.CustomPhysics;
using UnityEngine;

namespace Game.Scripts.Features.Player.Services
{
    public class PlayerMovementService: IDisposable
    {
        private PlayerModel _playerModel;
        private CustomPhysicsFacade2D _playerPhysics;

        public PlayerMovementService(PlayerModel playerModel)
        {
            _playerModel = playerModel;
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
            _playerModel.ChangePosition(position);
        }

        private void ChangeVelocity(float instantVelocity)
        {
            _playerModel.ChangeVelocity(instantVelocity);
        }

        private void ChangeRotation(float instantRotation)
        {
            _playerModel.ChangeRotation(instantRotation);
        }

        public void Dispose()
        {
            _playerPhysics.OnPositionChanged -= ChangePosition;
            _playerPhysics.OnRotationChanged -= ChangeRotation;
            _playerModel.OnVelocityChanged -= ChangeVelocity;
        }
    }
}