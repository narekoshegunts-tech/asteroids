using Game.Scripts.UI.ViewModels;
using Game.Scripts.UI.Views;
using MVVM;

namespace Game.Scripts.UI.Binders
{
    public class LaserChargeIndicatorBinder : IBinder
    {
        private readonly LaserChargeIndicatorView _view;
        private readonly LaserChargeIndicatorViewModel _viewModel;

        public LaserChargeIndicatorBinder(LaserChargeIndicatorView view, LaserChargeIndicatorViewModel viewModel)
        {
            _view = view;
            _viewModel = viewModel;
        }

        public void Bind()
        {
            _view.Initialize(_viewModel.MaxLaserAttacks, _viewModel.ChargeTime);
            _viewModel.OnLaserAttack += _view.UpdateLaserAttacks;
            _view.OnChargeComplete += _viewModel.OnLaserCharge;
        }

        public void Unbind()
        {
            _viewModel.OnLaserAttack -= _view.UpdateLaserAttacks;
            _view.OnChargeComplete -= _viewModel.OnLaserCharge;
        }
    }
}