using UnityEngine;
using Zenject;

namespace Game.Scripts.Common.CameraServices
{
    public class CameraUtils
    {
        [Inject] private Camera _camera;


        public Vector2 GetOffscreenPosition(float offset = 10f)
        {
            Vector2 cameraPosition = _camera.gameObject.transform.position; 
            
            GetScreenSize(out float halfHeight, out float halfWidth);
            
            float visibleRadius = Mathf.Sqrt(halfHeight * halfHeight + halfWidth * halfWidth);
            
            float angle = Random.Range(0, 2 * Mathf.PI);
            
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            
            return cameraPosition + direction * (visibleRadius + offset); 
        }

        public Vector2 GetScreenRandomPosition()
        {
            GetScreenSize(out float halfHeight, out float halfWidth);
            
            float xPosition = Random.Range(-halfWidth, halfWidth);
            float yPosition = Random.Range(-halfHeight, halfHeight);
            
            return new Vector2(xPosition, yPosition);
        }

        public void GetScreenSize(out float halfHeight, out float halfWidth)
        {
            halfHeight = _camera.orthographicSize;
            halfWidth = _camera.aspect * halfHeight;
        }
    }
}