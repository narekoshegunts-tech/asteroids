using System;
using Game.Scripts.Features.Player.Data;

namespace Game.Scripts.Features.Player.Model
{
    public class PlayerModel
    {
        private PlayerHealthModel _playerHealthModel;
        private PlayerMovementModel _playerMovementModel;
        private PlayerLaserAttackModel _playerLaserAttackModel;
        
        public PlayerModel(PlayerHealthModel playerHealthModel, PlayerMovementModel playerMovementModel,
            PlayerLaserAttackModel playerLaserAttackModel)
        {
            _playerHealthModel = playerHealthModel;
            _playerMovementModel = playerMovementModel;
            _playerLaserAttackModel = playerLaserAttackModel;
        }
    }
}