using Game.Scripts.Common.CameraServices;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class CameraInstallers: MonoInstaller
    {
        [SerializeField] private Camera _camera;

        public override void InstallBindings()
        {
            BindCamera();
            BindCameraDataService();
            BindCameraService();
        }

        private void BindCamera()
        {
            Container
                .Bind<Camera>()
                .FromInstance(_camera)
                .AsSingle()
                .NonLazy();
        }

        private void BindCameraDataService()
        {
            Container
                .Bind<CameraDataService>()
                .AsSingle()
                .NonLazy();
        }
        
        private void BindCameraService()
        {
            Container
                .Bind<CameraUtils>()
                .AsSingle();
        }
        
    }
}