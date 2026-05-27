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
            _viewModel.OnFillAmountChanged += _view.UpdateFillAmount;
            _viewModel.OnLaserAttacksChanged += _view.UpdateCurrentLaserAttacks;
        }

        public void Unbind()
        {
            _viewModel.OnFillAmountChanged -= _view.UpdateFillAmount;
            _viewModel.OnLaserAttacksChanged -= _view.UpdateCurrentLaserAttacks;
        }
    }
}