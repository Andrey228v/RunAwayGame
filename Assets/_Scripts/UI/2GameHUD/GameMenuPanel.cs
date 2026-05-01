using Assets._Scripts.EventBusGame;
using Assets._Scripts.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets.Scripts.UI
{
    public class GameMenuPanel : MonoBehaviour, IPanel
    {
        [Header("Buttons")]
        [SerializeField] private Button _backToGameButton;
        [SerializeField] private Button _backToMenuButton;

        private IEventPublisher _eventBus;

        public bool IsVisible { get; private set; }

        public string Name { get; set; }

#if UNITY_EDITOR
        public void OnValidate()
        {
            if (_backToGameButton == null)
            {
                Debug.LogError($"{gameObject.name}: _backToGameButton is not set!", this);
            }

            if (_backToMenuButton == null)
            {
                Debug.LogError($"{gameObject.name}: _backToMenuButton is not set!", this);
            }
        }
#endif
        [Inject]
        public void Construct(IEventPublisher eventBus)
        {
            _eventBus = eventBus;
        }

        private void OnEnable()
        {
            _backToGameButton.onClick.AddListener(ClickBackToGame);
            _backToMenuButton.onClick.AddListener(ClickBackToMenu);
        }

        private void OnDisable()
        {
            _backToGameButton.onClick.RemoveListener(ClickBackToGame);
            _backToMenuButton.onClick.RemoveListener(ClickBackToMenu);
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

        private void ClickBackToGame()
        {
            _eventBus.Publish(new TransitToPanelEvent { windowName = "GameInterfacePanel" });
        }

        private void ClickBackToMenu()
        {
            _eventBus.Publish(new TransitToWindowEvent { });
        }
    }
}
