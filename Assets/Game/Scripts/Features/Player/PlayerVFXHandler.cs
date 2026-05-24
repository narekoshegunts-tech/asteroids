using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerVFXHandler: MonoBehaviour
    {
        [Inject] private PlayerStateService _playerStateService;
        
        [SerializeField] private ParticleSystem _invulnerabilityParticles;

        private void OnEnable()
        {
            _playerStateService.OnInvulnerabilityStart += StartInvulnerabilityEffect;
            _playerStateService.OnInvulnerabilityEnd += StopInvulnerabilityEffect;
        }

        private void OnDisable()
        {
            _playerStateService.OnInvulnerabilityStart -= StartInvulnerabilityEffect;
            _playerStateService.OnInvulnerabilityEnd -= StopInvulnerabilityEffect;
        }

        private void Start()
        {
            _invulnerabilityParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        private void StartInvulnerabilityEffect()
        {
            _invulnerabilityParticles.Play();
        }

        private void StopInvulnerabilityEffect()
        {
            _invulnerabilityParticles.Stop();
        }
    }
}