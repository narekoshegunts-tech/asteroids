using System.Threading;
using Game.Scripts.Common.ObjectPool;
using Game.Scripts.Features.Enemies.UFO.Data;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Enemies.UFO
{
    public class UfoSpawnerService: EnemySpawnerService<Ufo>
    {
        private UfoDataService _ufoDataService;
        
        private UfoData _ufoData;

        private CancellationTokenSource _cts;
        
        protected override string PrefabPath => "Prefabs/Enemies/Ufo";
        protected override float SpawnCooldown => _ufoDataService.SpawnCooldown;
        protected override int PoolSize => _ufoDataService.PoolSize;
        
        [Inject]
        private void Construct(UfoDataService ufoDataService)
        {
            _ufoDataService = ufoDataService;
            
            _ufoData = _ufoDataService.UfoData;
        }


        protected override bool CanSpawn() => true;
        

        protected override void Spawn()
        {
            if (_pool.TryGet(out Ufo ufo))
            {
                Vector2 spawnPosition = _cameraUtils.GetOffscreenPosition();
                
                ufo.Initialize(spawnPosition, _ufoData);
                ufo.OnDead += OnUfoDestroyed;

                RaiseOnAnyEnemySpawned(ufo);
            }
        }

        private void OnUfoDestroyed(Enemy ufo)
        {
            ReturnToPool(ufo);
            ufo.OnDead -= OnUfoDestroyed;
        }

    }
}