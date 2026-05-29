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
        
        private CustomInputSystem _customInputSystem;

        public event Action OnAccelerationStart;
        public event Action OnAccelerationEnd;
        
        public Vector2 Direction => _customPhysicsFacade.Direction;
        
        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacadeFactory,
            CustomInputSystem customInputSystem, PlayerDataService playerDataService,
            PlayerStateService playerStateService, PlayerMovementService playerMovementService)
        {
            _customPhysicsFacade = customPhysicsFacadeFactory.Create(transform);
            _customInputSystem = customInputSystem;
            
            _customInputSystem.SetTargetTransform(transform);
            
            _acceleration = playerDataService.Acceleration;
            
            _playerStateService = playerStateService;
            _playerMovementService = playerMovementService;
            
            _playerMovementService.Initialize(_customPhysicsFacade);
        }

        private void OnEnable()
        {
            SubscribeToInputSystem();
        }

        private void SubscribeToInputSystem()
        {
            _customInputSystem.OnAccelerationKeyPressedDown += OnAccelerationKeyPressedDown;
            _customInputSystem.OnAccelerationKeyPressed += OnAccelerationKeyPressed;
            _customInputSystem.OnAccelerationKeyPressedUp += OnAccelerationKeyPressedUp;
        }

        private void UnSubscribeFromInputSystem()
        {
            _customInputSystem.OnAccelerationKeyPressedDown -= OnAccelerationKeyPressedDown;
            _customInputSystem.OnAccelerationKeyPressed -= OnAccelerationKeyPressed;
            _customInputSystem.OnAccelerationKeyPressedUp -= OnAccelerationKeyPressedUp;
        }

        private void FixedUpdate()
        {
            
            Vector2 direction = _customInputSystem.GetDirection();

            if (_playerStateService.CanMove)
                _customPhysicsFacade.ApplyRotation(direction);

            _customPhysicsFacade.FixedUpdate();
        }

        private void OnAccelerationKeyPressedDown()
        {
            if (!_playerStateService.CanMove)
                return;
            OnAccelerationStart?.Invoke();
        }

        private void OnAccelerationKeyPressed()
        {
            if (!_playerStateService.CanMove)
            {
                _customPhysicsFacade.ApplyAcceleration(0);
                OnAccelerationEnd?.Invoke();

                return;
            }
            
            _customPhysicsFacade.ApplyAcceleration(_acceleration);
        }

        private void OnAccelerationKeyPressedUp()
        {
            _customPhysicsFacade.ApplyAcceleration(0);
            OnAccelerationEnd?.Invoke();
        }

        public CustomPhysicsFacade2D GetCustomPhysicsFacade2D()
        {
            return _customPhysicsFacade;
        }
        
        private void OnDisable()
        {
            UnSubscribeFromInputSystem();
        }
    }
}
