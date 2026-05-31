using System;
using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using Game.Scripts.Common.CustomInput;
using Game.Scripts.Features.Interfaces;
using Game.Scripts.Features.Player.Data;
using Game.Scripts.Features.Player.Interfaces;
using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerMovement: MonoBehaviour, ITeleportable, ICollisionable, IPlayerDirection
    {
        private PlayerStateService _playerStateService;
        private PlayerMovementService _playerMovementService;
        
        private CustomPhysicsFacade2D _customPhysicsFacade;

        private float _acceleration;

        public event Action OnAccelerationStart;
        public event Action OnAccelerationEnd;

        private Vector2 _direction;
        public Vector2 Direction => _customPhysicsFacade.Direction;
        
        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacadeFactory,
            CustomInputSystem customInputSystem, PlayerDataService playerDataService,
            PlayerStateService playerStateService, PlayerMovementService playerMovementService)
        {
            _customPhysicsFacade = customPhysicsFacadeFactory.Create(transform);

            
            _acceleration = playerDataService.Acceleration;
            
            _playerStateService = playerStateService;
            _playerMovementService = playerMovementService;
            
            _playerMovementService.Initialize(_customPhysicsFacade);
        }

        public void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private void FixedUpdate()
        {

            if (_playerStateService.CanMove)
                _customPhysicsFacade.ApplyRotation(_direction);

            _customPhysicsFacade.FixedUpdate();
        }

        public void OnAccelerationKeyPressedDown()
        {
            if (!_playerStateService.CanMove)
                return;
            OnAccelerationStart?.Invoke();
        }

        public void OnAccelerationKeyPressed()
        {
            if (!_playerStateService.CanMove)
            {
                _customPhysicsFacade.ApplyAcceleration(0);
                OnAccelerationEnd?.Invoke();

                return;
            }
            
            _customPhysicsFacade.ApplyAcceleration(_acceleration);
        }

        public void OnAccelerationKeyPressedUp()
        {
            _customPhysicsFacade.ApplyAcceleration(0);
            OnAccelerationEnd?.Invoke();
        }

        public CustomPhysicsFacade2D GetCustomPhysicsFacade2D()
        {
            return _customPhysicsFacade;
        }
    }
}
