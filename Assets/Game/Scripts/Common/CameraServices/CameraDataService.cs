using Game.Scripts.Common.CameraServices.Data;
using Game.Scripts.Common.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Common.CameraServices
{
    public class CameraDataService: JsonConfigLoader<CameraData>
    {
        private const string ResourcePath = "Configs/camera";
        
        private Camera _camera;

        public CameraDataService(Camera camera) : base(ResourcePath)
        {
            _camera = camera;
        }

        [Inject]
        private void Construct(Camera camera)
        {
            _camera = camera;
            LoadConfig();
        }

        private void LoadConfig()
        {
            CameraData data = LoadRoot();
            _camera.orthographicSize = data.OrthographicSize;
        }
    }
}