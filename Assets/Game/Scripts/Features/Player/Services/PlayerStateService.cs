using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Scripts.Features.Player.Services
{
    public class PlayerStateService
    {
        private CancellationTokenSource _cts;
        private UniTask _currentInvulnerabilityTask;
        
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
        }

        public void ApplyInvulnerability()
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            
            _currentInvulnerabilityTask = ApplyInvulnerabilityTask(_invulnerabilityDuration);
        }

        private async UniTask ApplyInvulnerabilityTask(float duration)
        {
            if (_cts.IsCancellationRequested)
                return;

            try
            {
                EnterInvulnerabilityState();

                await UniTask.Delay(TimeSpan.FromSeconds(duration),
                    cancellationToken: _cts.Token);

                ExitInvulnerabilityState();
            }
            catch (OperationCanceledException)
            {
                ExitInvulnerabilityState();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            
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