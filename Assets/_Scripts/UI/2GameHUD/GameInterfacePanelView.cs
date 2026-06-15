using Assets._Scripts.EventBusGame;
using Assets._Scripts.Loger;
using Assets._Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets.Scripts.UI
{
    public class GameInterfacePanelView : MonoBehaviour, IPanel
    {
        [Header("Buttons")]
        [SerializeField] private Button _menuButton;
        [SerializeField] private Button _loadButton;
        [SerializeField] private Button _soundButton;
        [SerializeField] private Button _saveButton;

        [Header("Game data")]
        [SerializeField] private TextMeshProUGUI _coinsCounter;
        [SerializeField] private TextMeshProUGUI _timer;

        private IEventPublisher _eventBus;
        private IGameLogger _gameLogger;

        public bool IsVisible { get; private set; }

        public string Name { get; set; }

#if UNITY_EDITOR
        public void OnValidate()
        {
            if (_menuButton == null)
            {
                Debug.LogError($"{gameObject.name}: _menuButton is not set!", this);
            }

            if (_loadButton == null)
            {
                Debug.LogError($"{gameObject.name}: _loadButton is not set!", this);
            }

            if (_soundButton == null)
            {
                Debug.LogError($"{gameObject.name}: _soundButton is not set!", this);
            }

            if (_coinsCounter == null)
            {
                Debug.LogError($"{gameObject.name}: _coinsCounter is not set!", this);
            }

            if (_timer == null)
            {
                Debug.LogError($"{gameObject.name}: _timer is not set!", this);
            }
        }
#endif

        [Inject]
        public void Construct(IEventPublisher eventBus,
            IGameLogger gameLogger)
        {
            _eventBus = eventBus;
            _gameLogger = gameLogger;
        }

        private void OnEnable()
        {
            _menuButton.onClick.AddListener(ClickMenuButton);
            _loadButton.onClick.AddListener(ClickLoadButton);
            _soundButton.onClick.AddListener(ClickSoundButton);
            _saveButton.onClick.AddListener(ClickSaveButton);


        }

        private void OnDisable()
        {
            _menuButton.onClick.RemoveListener(ClickMenuButton);
            _loadButton.onClick.RemoveListener(ClickLoadButton);
            _soundButton.onClick.RemoveListener(ClickSoundButton);
            _saveButton.onClick.RemoveListener(ClickSaveButton);
        }

        public void Show()
        {
            gameObject.SetActive(true);
            IsVisible = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            IsVisible = false;
        }

        private void ClickMenuButton()
        {
            _gameLogger.Log("ClickMenuButton", "Event");
            _eventBus.Publish(new TransitToPanelEvent { windowName = "GameMenuPanel"});
        }

        private void ClickLoadButton()
        {
            _gameLogger.Log("ClickLoadButton", "Event");
            _eventBus.Publish(new LoadGameEvent { });
        }

        private void ClickSoundButton()
        {
            _gameLogger.Log("ClickSoundButton", "Event");
            _eventBus.Publish(new ButtonSoundChangeStateEvent { });
        }

        private void ClickSaveButton()
        {
            _gameLogger.Log("ClickSaveButton", "Event");
            _eventBus.Publish(new SaveGameEvent { });
        }

        public void SetCoinsCountText(int actualCoin, int addCoin)
        {
            _coinsCounter.text = actualCoin.ToString();
        }
    }
}

