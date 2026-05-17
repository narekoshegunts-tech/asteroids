using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Features.Enemies.Asteroids
{
    public class AsteroidVisual: MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        [SerializeField] private List<Sprite> _sprites;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _spriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Count)];
        }
    }
}