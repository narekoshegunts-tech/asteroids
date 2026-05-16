using Game.Scripts.Common.CameraServices.Data;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Common.CameraServices
{
    public class CameraDataService
    {
        private const string ResourcePath = "Configs/camera";
        
        private Camera _camera;

        [Inject]
        public CameraDataService(Camera camera)
        {
            _camera = camera;
            LoadConfig();
        }

        private void LoadConfig()
        {
            TextAsset config = Resources.Load<TextAsset>(ResourcePath);
            CameraData data = JsonConvert.DeserializeObject<CameraData>(config.text);
            _camera.orthographicSize = data.OrthographicSize;
        }
    }
}