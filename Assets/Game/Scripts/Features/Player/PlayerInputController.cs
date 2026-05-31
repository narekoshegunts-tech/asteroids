using System;
using Game.Scripts.Common.CustomInput;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerInputController: MonoBehaviour
    {
        private CustomInputSystem _customInputSystem;
        private PlayerMovement _playerMovement;

        [Inject]
        private void Construct(CustomInputSystem customInputSystem, PlayerMovement playerMovement)
        {
            _customInputSystem = customInputSystem;
            _customInputSystem.SetTargetTransform(transform);
            
            _playerMovement = playerMovement;
        }
        
        private void OnEnable()
        {
            SubscribeToInputSystem();
        }

        private void FixedUpdate()
        {
            _playerMovement.SetDirection(_customInputSystem.GetDirection());
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
        
        private void OnAccelerationKeyPressedDown()
        {
            _playerMovement.OnAccelerationKeyPressedDown();
        }

        private void OnAccelerationKeyPressed()
        {
            _playerMovement.OnAccelerationKeyPressed();
        }

        private void OnAccelerationKeyPressedUp()
        {
            _playerMovement.OnAccelerationKeyPressedUp();
        }

        private void OnDisable()
        {
            UnSubscribeFromInputSystem();
        }
    }
}