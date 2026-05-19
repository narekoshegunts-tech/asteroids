using System;
using Game.Scripts.Features.UI.ViewModels;
using Game.Scripts.UI.Views;
using MVVM;
using UnityEngine;

namespace Game.Scripts.UI.Binders
{
    public class HealthBinder : IBinder
    {
        private readonly HealthView _view;
        private readonly HealthViewModel _viewModel;

        public HealthBinder(HealthView view, HealthViewModel viewModel)
        {
            _view = view;
            _viewModel = viewModel;
        }

        public void Bind()
        {
            _view.Initialize(_viewModel.MaxHealth);
            _viewModel.OnCurrentHealthChanged += _view.UpdateHearts;
        }

        public void Unbind()
        {
            _viewModel.OnCurrentHealthChanged -= _view.UpdateHearts;
        }
    }
}