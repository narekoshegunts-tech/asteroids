using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using Game.Scripts.Common.CustomInput;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerMovement: MonoBehaviour
    {
        CustomPhysicsFacade2D _customPhysicsFacade;

        [SerializeField] private float _acceleration;
        
        CustomInputSystem _customInputSystem;
        
        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacadeFactory,
            CustomInputSystem customInputSystem)
        {
            _customPhysicsFacade = customPhysicsFacadeFactory.Create(transform);
            _customInputSystem = customInputSystem;
            
            _customInputSystem.SetTargetTransform(transform);
        }

        private void OnEnable()
        {
            _customInputSystem.OnAccelerationKeyPressed += OnAccelerationKeyPressed;
        }

        private void OnDisable()
        {
            _customInputSystem.OnAccelerationKeyPressed -= OnAccelerationKeyPressed;
        }

        void Update()
        {
            Vector2 direction = _customInputSystem.GetDirection();
            
            _customPhysicsFacade.ApplyRotation(direction);

            _customPhysicsFacade.Update();
        }

        private void OnAccelerationKeyPressed()
        {
            _customPhysicsFacade.ApplyAcceleration(_acceleration);
        }
    }
}
