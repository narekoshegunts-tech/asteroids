using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerMovement: MonoBehaviour
    {
        CustomPhysicsFacade2D _customPhysicsFacade;

        [SerializeField] private float _acceleration;
        private Camera _cam;
        
        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacadeFactory)
        {
            _customPhysicsFacade = customPhysicsFacadeFactory.Create(transform);
        }
        
        private void Start()
        {
            _cam = Camera.main; // убрать глобальный поиск
        }

        void Update()
        {
            _customPhysicsFacade.ApplyRotation(GetDirection());

            if (Input.GetKey(KeyCode.W))
            {
                _customPhysicsFacade.ApplyAcceleration(_acceleration);
            }

            _customPhysicsFacade.Update();
        }

        // убрать в самописный InputSystem
        private Vector2 GetDirection()
        {
            Vector2 mousePosition = _cam.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction =  mousePosition - new Vector2(transform.position.x, transform.position.y);
            
            return direction.normalized;
        }

    }
}
