using UnityEngine;
using Zenject;

namespace CustomPhysics
{
    public class DebugPhysics: MonoBehaviour
    {
        CustomPhysicsFacade2D _customPhysicsFacade2D;

        [Inject]
        private void Construct(CustomPhysicsFacade2D.Factory customPhysicsFacade2DFactory)
        {
            _customPhysicsFacade2D = customPhysicsFacade2DFactory.Create(transform);
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                _customPhysicsFacade2D.ApplyAcceleration(new Vector2(1, 1));
            }

            _customPhysicsFacade2D.Update(Time.deltaTime);
        }
    }
}