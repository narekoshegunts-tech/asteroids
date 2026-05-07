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

        void Update()
        {
            Vector2 direction = _customInputSystem.GetDirection();
            
            _customPhysicsFacade.ApplyRotation(direction);

            if (_customInputSystem.IsAccelerationKeyPressed())
            {
                _customPhysicsFacade.ApplyAcceleration(_acceleration);
            }

            _customPhysicsFacade.Update();
        }
    }
}
