using Game.Scripts.CustomPhysics;
using Game.Scripts.CustomPhysics.Factories;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player.Attack
{
    public class Bullet: MonoBehaviour
    {
        private CustomPhysicsFacade2D _customPhysicsFacade2D;
        [SerializeField] private float _speed; 
        

        [Inject]
        private void Construct(CustomPhysicsFacade2DFactory customPhysicsFacade2DFactory)
        {
            _customPhysicsFacade2D = customPhysicsFacade2DFactory.Create(transform);
        }

        public void Init(Vector3 startPosition, Vector2 direction)
        {
            _customPhysicsFacade2D.ApplyPosition(startPosition);
            _customPhysicsFacade2D.ApplyRotation(direction);
                        
            _customPhysicsFacade2D.ApplyVelocity(_speed);
        }

        public void Update()
        {
            _customPhysicsFacade2D.Update();
        }
    }
}