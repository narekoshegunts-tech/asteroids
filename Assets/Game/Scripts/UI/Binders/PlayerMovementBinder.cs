using Game.Scripts.UI.ViewModels;
using Game.Scripts.UI.Views;
using MVVM;

namespace Game.Scripts.UI.Binders
{
    public class PlayerMovementBinder : IBinder
    {
        private readonly PlayerMovementView _view;
        private readonly PlayerMovementViewModel _viewModel;

        public PlayerMovementBinder(PlayerMovementView view, PlayerMovementViewModel viewModel)
        {
            _view = view;
            _viewModel = viewModel;
        }

        public void Bind()
        {
            _view.Initialize();
            _viewModel.OnPositionChanged += _view.UpdatePosition;
            _viewModel.OnRotationChanged += _view.UpdateRotation;
            _viewModel.OnVelocityChanged += _view.UpdateVelocity;
        }

        public void Unbind()
        {
            _viewModel.OnPositionChanged -= _view.UpdatePosition;
            _viewModel.OnRotationChanged -= _view.UpdateRotation;
            _viewModel.OnVelocityChanged -= _view.UpdateVelocity;
        }
    }
}