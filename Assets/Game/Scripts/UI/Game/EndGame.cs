using Game.Scripts.SDK;
using Game.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.UI.Game
{
    public class EndGame : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        [SerializeField] private Button _restartButton;
        
        private SignalBus _signalBus;
        private YandexBannerService _yandexBannerService;

        [Inject]
        private void Construct(SignalBus signalBus, YandexBannerService yandexBannerService)
        {
            _signalBus = signalBus;
            _yandexBannerService = yandexBannerService;
        }
        

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        private void Start()
        {
            _restartButton.interactable = false;
            _canvasGroup.alpha = 0;
        }

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartClick);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartClick);
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _restartButton.interactable = true;
            _yandexBannerService.Show();
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0f;
            _restartButton.interactable = false;
            _yandexBannerService.Hide();
        }

        private void OnRestartClick()
        {
            _signalBus.Fire<RestartRequestSignal>();
            Hide();
        }
    }
}