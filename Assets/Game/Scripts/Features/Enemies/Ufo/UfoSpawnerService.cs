using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Common.CameraServices;
using Game.Scripts.Common.ObjectPool;
using Game.Scripts.Features.Enemies.Asteroids;
using Game.Scripts.Features.Enemies.Asteroids.Data;
using Game.Scripts.Features.Enemies.UFO.Data;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Scripts.Features.Enemies.UFO
{
    public class UfoSpawnerService
    {
        [Inject] private CameraUtils _cameraService;
        
        private UfoData _ufoData;
        private ObjectPool<Ufo> _ufoPool;
        
        private const string PrefabPath = "Prefabs/Enemies/Ufo";
        
        private Ufo _ufoPrefab;

        private float _ufoSpawnCooldown;

        private CancellationTokenSource _cts;
        
        private int _currentUfoCount;
        

        [Inject]
        private void Construct(ObjectPoolFactory objectPoolFactory, UfoDataService ufoDataService)
        {
            _ufoPrefab = Resources.Load<Ufo>(PrefabPath);
            Debug.Log(_ufoPrefab);
            
            _ufoSpawnCooldown = ufoDataService.SpawnCooldown;
            
            _ufoData = ufoDataService.UfoData;
            
            GameObject container = new GameObject("UfoPool");
            _ufoPool = objectPoolFactory.Create(_ufoPrefab, container, ufoDataService.PoolSize);
        }
        
        public void StartSpawning()
        {
            _cts = new CancellationTokenSource();
            SpawnLoop().Forget();
        }
        
        
        private async UniTaskVoid SpawnLoop()
        {
            while (_cts.IsCancellationRequested == false)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(_ufoSpawnCooldown),
                    ignoreTimeScale: false,
                    cancellationToken: _cts.Token);
                
                SpawnUfo();
            }
        }

        private void SpawnUfo()
        {
            if (_ufoPool.TryGet(out Ufo ufo))
            {
                Vector2 spawnPosition = _cameraService.GetOffscreenPosition();
                Vector2 targetPosition = _cameraService.GetScreenRandomPosition();
                ufo.Initialize(spawnPosition, _ufoData);
                ufo.OnDestroy += OnUfoDestroyed;
            }
        }

        private void OnUfoDestroyed(Enemy ufo)
        {
            _ufoPool.Return(ufo as Ufo);
            ufo.OnDestroy -= OnUfoDestroyed;
        }

        public void Destroy()
        {
            _cts.Cancel();
            _cts.Dispose();
        }
    }
}