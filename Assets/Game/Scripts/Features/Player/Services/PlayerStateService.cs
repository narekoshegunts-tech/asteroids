using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Features.Player.Services
{
    public class PlayerStateService
    {
        private CancellationTokenSource _cts;
        
        private float _invulnerabilityDuration;
        
        public event Action OnInvulnerabilityStart;
        public event Action OnInvulnerabilityEnd;
        
        public bool CanMove { get; private set; }
        public bool CanAttack { get; private set; }
        public bool IsInvulnerable { get; private set; }
        

        public PlayerStateService(PlayerModel playerModel)
        {
            CanMove = true;
            CanAttack = true;
            IsInvulnerable = false;
            
            _invulnerabilityDuration = playerModel.InvulnerabilityDuration;
            
            _cts = new CancellationTokenSource();
        }

        public void ApplyInvulnerability()
        {
            ApplyInvulnerabilityTask(_invulnerabilityDuration).Forget();
        }

        private async UniTask ApplyInvulnerabilityTask(float duration)
        {
            EnterInvulnerabilityState();
            
            await UniTask.Delay(TimeSpan.FromSeconds(duration),
                cancellationToken: _cts.Token);
            
            ExitInvulnerabilityState();
        }

        private void EnterInvulnerabilityState()
        {
            OnInvulnerabilityStart?.Invoke();
            
            CanMove = false;
            CanAttack = false;
            IsInvulnerable = true;
        }
        
        private void ExitInvulnerabilityState()
        {
            OnInvulnerabilityEnd?.Invoke();
            
            CanMove = true;
            CanAttack = true;
            IsInvulnerable = false;
        }
    }
}