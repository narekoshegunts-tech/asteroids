using Game.Scripts.Features.Enemies.UFO.Data;
using UnityEngine;

namespace Game.Scripts.Features.Enemies.UFO
{
    [RequireComponent(typeof(UfoMovement))]
    public class Ufo: Enemy
    {
        
        private UfoMovement _ufoMovement;


        private void Awake()
        {
            _ufoMovement = GetComponent<UfoMovement>();
        }

        public void Initialize(Vector3 startPosition, UfoData data)
        {
            EnemyType = EnemyType.Ufo;
            
            var scale = Random.Range(data.MinScale, data.MaxScale);
            transform.localScale = scale * Vector3.one;
            
            _ufoMovement.Init(startPosition, data);
        }
    }
}