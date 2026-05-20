using Game.Scripts.UI.ViewModels;
using Game.Scripts.UI.Views;
using MVVM;

namespace Game.Scripts.UI.Binders
{
    public class ScoreBinder: IBinder
    {
        private readonly ScoreView _view;
        private readonly ScoreViewModel _viewModel;

        public ScoreBinder(ScoreView view, ScoreViewModel scoreViewModel)
        {
            _view = view;
            _viewModel = scoreViewModel;
        }
        public void Bind()
        {
            _viewModel.OnScoreChanged += _view.UpdateScore;
        }

        public void Unbind()
        {
            _viewModel.OnScoreChanged -= _view.UpdateScore;
        }
    }
}