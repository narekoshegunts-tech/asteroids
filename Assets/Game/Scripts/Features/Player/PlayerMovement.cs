using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using Game.Scripts.Common.CustomInput;
using Game.Scripts.Features.Interfaces;
using Game.Scripts.Features.Player.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerMovement: MonoBehaviour, ITeleportable, ICollisionable
    {
        [Inject] private PlayerModel _playerModel;
        
        private CustomPhysicsFacade2D _customPhysicsFacade;

        private float _acceleration;
        
        private CustomInputSystem _customInputSystem;
        
        [SerializeField] private ParticleSystem _accelerationParticles;
        
        public Vector2 Direction => _customPhysicsFacade.Direction;
        
        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacadeFactory,
            CustomInputSystem customInputSystem, PlayerDataService playerDataService)
        {
            _customPhysicsFacade = customPhysicsFacadeFactory.Create(transform);
            _customInputSystem = customInputSystem;
            
            _customInputSystem.SetTargetTransform(transform);
            
            _acceleration = playerDataService.Acceleration;
        }

        private void Awake()
        {
            _accelerationParticles.Stop();
        }

        private void OnEnable()
        {
            SubcribeToInputSystem();
        }

        private void OnDisable()
        {
            UnSubcribeFromInputSystem();
        }

        private void SubcribeToInputSystem()
        {
            _customInputSystem.OnAccelerationKeyPressedDown += OnAccelerationKeyPressedDown;
            _customInputSystem.OnAccelerationKeyPressed += OnAccelerationKeyPressed;
            _customInputSystem.OnAccelerationKeyPressedUp += OnAccelerationKeyPressedUp;
        }

        private void UnSubcribeFromInputSystem()
        {
            _customInputSystem.OnAccelerationKeyPressedDown -= OnAccelerationKeyPressedDown;
            _customInputSystem.OnAccelerationKeyPressed -= OnAccelerationKeyPressed;
            _customInputSystem.OnAccelerationKeyPressedUp -= OnAccelerationKeyPressedUp;
        }

        private void FixedUpdate()
        {
            Vector2 direction = _customInputSystem.GetDirection();

            _customPhysicsFacade.ApplyRotation(direction);
            
            _playerModel.ChangeRotation(_customPhysicsFacade.GetRotation());
            _playerModel.ChangePosition(_customPhysicsFacade.GetPosition());

            _customPhysicsFacade.FixedUpdate();
        }

        private void OnAccelerationKeyPressedDown()
        {
            _accelerationParticles.Play();
        }

        private void OnAccelerationKeyPressed()
        {
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
