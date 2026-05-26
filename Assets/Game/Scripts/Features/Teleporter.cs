using Game.Scripts.Common.CameraServices;
using Game.Scripts.CustomPhysics;
using Game.Scripts.Features.Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Teleporter: MonoBehaviour
    {
        private CameraUtils _cameraUtils;
        
        private BoxCollider2D _collider;
        
        private float _halfHeight;
        private float _halfWidth;

        [Inject]
        private void Construct(CameraUtils cameraUtils)
        {
            _cameraUtils = cameraUtils;
        }

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _collider.isTrigger = true;
            SetColliderSize();
        }

        private void SetColliderSize()
        {
            _cameraUtils.GetScreenSize(out _halfHeight, out _halfWidth);
            _collider.size = new Vector2(_halfWidth * 2, _halfHeight * 2);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent<ITeleportable>(out var teleporter))
            {
                Teleport(teleporter.GetCustomPhysicsFacade2D());
            }
        }

        private void Teleport(CustomPhysicsFacade2D physicsFacade2D)
        {
            Vector2 targetPosition = physicsFacade2D.GetPosition();
            
            float distanceToTeleporterY = targetPosition.y - transform.position.y;
            float distanceToTeleporterX = targetPosition.x - transform.position.x;

            float xPos = targetPosition.x;
            float yPos = targetPosition.y;

            if (distanceToTeleporterY > _halfHeight || distanceToTeleporterY < -1 * _halfHeight)
            {
                yPos = transform.position.y - distanceToTeleporterY;
            }

            if (distanceToTeleporterX > _halfWidth || distanceToTeleporterX < -1 * _halfWidth)
            {
                xPos = transform.position.x - distanceToTeleporterX;
            }
            
            physicsFacade2D.ApplyPosition(new Vector2(xPos, yPos));
        }
    }
}