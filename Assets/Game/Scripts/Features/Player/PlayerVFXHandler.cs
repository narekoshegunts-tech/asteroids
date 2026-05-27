using Game.Scripts.Features.Player.Services;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Features.Player
{
    public class PlayerVFXHandler: MonoBehaviour
    {
        private PlayerStateService _playerStateService;
        private PlayerMovement _playerMovement;
        
        [SerializeField] private ParticleSystem _invulnerabilityParticles;
        [SerializeField] private ParticleSystem _accelerationParticles;

        [Inject]
        private void Construct(PlayerStateService playerStateService, PlayerMovement playerMovement)
        {
            _playerStateService = playerStateService;
            _playerMovement = playerMovement;
        }
        
        private void OnEnable()
        {
            _playerStateService.OnInvulnerabilityStart += StartInvulnerabilityEffect;
            _playerStateService.OnInvulnerabilityEnd += StopInvulnerabilityEffect;
            
            _playerMovement.OnAccelerationStart += StartAccelerationEffect;
            _playerMovement.OnAccelerationEnd += StopAccelerationEffect;
        }

        private void OnDisable()
        {
            _playerStateService.OnInvulnerabilityStart -= StartInvulnerabilityEffect;
            _playerStateService.OnInvulnerabilityEnd -= StopInvulnerabilityEffect;
            
            _playerMovement.OnAccelerationStart -= StartAccelerationEffect;
            _playerMovement.OnAccelerationEnd -= StopAccelerationEffect;
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

        private void StartAccelerationEffect()
        {
            _accelerationParticles.Play();
        }

        private void StopAccelerationEffect()
        {
            _accelerationParticles.Stop();
        }
    }
}