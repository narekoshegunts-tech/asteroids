using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.Scripts.Common.CameraServices;
using Game.Scripts.Common.ObjectPool;
using Game.Scripts.Features.Enemies.UFO.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies.UFO
{
    public class UfoSpawnerService: EnemySpawnerService<Ufo>
    {
        [Inject] private UfoDataService _ufoDataService;
        
        private UfoData _ufoData;

        private CancellationTokenSource _cts;
        
        protected override string PrefabPath => "Prefabs/Enemies/Ufo";
        protected override float SpawnCooldown => _ufoDataService.SpawnCooldown;
        protected override int PoolSize => _ufoDataService.PoolSize;
        
        
        protected override void Construct(ObjectPoolFactory objectPoolFactory)
        {
            base.Construct(objectPoolFactory);
            _ufoData = _ufoDataService.UfoData;
        }


        protected override bool CanSpawn() => true;
        

        protected override void Spawn()
        {
            if (_pool.TryGet(out Ufo ufo))
            {
                Vector2 spawnPosition = _cameraService.GetOffscreenPosition();
                
                ufo.Initialize(spawnPosition, _ufoData);
                ufo.OnDestroy += OnUfoDestroyed;
            }
        }

        private void OnUfoDestroyed(Enemy ufo)
        {
            ReturnToPool(ufo);
            ufo.OnDestroy -= OnUfoDestroyed;
        }

    }
}