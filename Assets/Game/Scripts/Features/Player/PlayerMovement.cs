using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using Game.Scripts.Common.CustomInput;
using Game.Scripts.Features.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerMovement: MonoBehaviour, ITeleportable, ICollisionable
    {
        private CustomPhysicsFacade2D _customPhysicsFacade;

        [SerializeField] private float _acceleration;
        
        private CustomInputSystem _customInputSystem;
        
        [SerializeField] private ParticleSystem _accelerationParticles;
        
        public Vector2 Direction => _customPhysicsFacade.Direction;
        
        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacadeFactory,
            CustomInputSystem customInputSystem)
        {
            _customPhysicsFacade = customPhysicsFacadeFactory.Create(transform);
            _customInputSystem = customInputSystem;
            
            _customInputSystem.SetTargetTransform(transform);
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

            _customPhysicsFacade.FixedUpdate();
        }

        private void OnAccelerationKeyPressedDown()
        {
            _accelerationParticles.Play();
        }

        private void OnAccelerationKeyPressed()
        {
            _customPhysicsFacade.ApplyAcceleration(_acceleration);
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
