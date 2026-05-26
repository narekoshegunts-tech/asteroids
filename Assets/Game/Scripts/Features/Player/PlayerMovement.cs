using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using Game.Scripts.Common.CustomInput;
using Game.Scripts.Features.Interfaces;
using Game.Scripts.Features.Player.Data;
using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerMovement: MonoBehaviour, ITeleportable, ICollisionable
    {
        private PlayerModel _playerModel;
        private PlayerStateService _playerStateService;
        
        private CustomPhysicsFacade2D _customPhysicsFacade;

        private float _acceleration;
        
        private CustomInputSystem _customInputSystem;
        
        [SerializeField] private ParticleSystem _accelerationParticles;
        
        public Vector2 Direction => _customPhysicsFacade.Direction;
        
        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacadeFactory,
            CustomInputSystem customInputSystem, PlayerDataService playerDataService,
            PlayerModel playerModel, PlayerStateService playerStateService)
        {
            _customPhysicsFacade = customPhysicsFacadeFactory.Create(transform);
            _customInputSystem = customInputSystem;
            
            _customInputSystem.SetTargetTransform(transform);
            
            _acceleration = playerDataService.Acceleration;
            _playerModel = playerModel;
            _playerStateService = playerStateService;
        }

        private void Awake()
        {
            _accelerationParticles.Stop();
        }

        private void OnEnable()
        {
            SubscribeToInputSystem();
        }

        private void OnDisable()
        {
            UnSubscribeFromInputSystem();
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
            
            _playerModel.ChangeRotation(_customPhysicsFacade.GetRotation());
            _playerModel.ChangePosition(_customPhysicsFacade.GetPosition());

            _customPhysicsFacade.FixedUpdate();
        }

        private void OnAccelerationKeyPressedDown()
        {
            if (!_playerStateService.CanMove)
                return;
            _accelerationParticles.Play();
        }

        private void OnAccelerationKeyPressed()
        {
            if (!_playerStateService.CanMove)
            {
                _customPhysicsFacade.ApplyAcceleration(0);
                _accelerationParticles.Stop();

                return;
            }
            
            _customPhysicsFacade.ApplyAcceleration(_acceleration);
            _playerModel.ChangeVelocity(_customPhysicsFacade.GetInstantVelocity());
        }

        private void OnAccelerationKeyPressedUp()
        {
            _customPhysicsFacade.ApplyAcceleration(0);
            _accelerationParticles.Stop();
        }

        public CustomPhysicsFacade2D GetCustomPhysicsFacade2D()
        {
            return _customPhysicsFacade;
        }
    }
}
