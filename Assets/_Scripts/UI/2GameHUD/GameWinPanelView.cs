using Assets._Scripts.EventBusGame;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets._Scripts.UI._2GameHUD
{
    public class GameWinPanelView : MonoBehaviour, IPanel
    {
        [Header("Buttons")]
        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private Button _reloudButton;

        private IEventPublisher _eventBus;

        public bool IsVisible { get; private set; }

        public string Name {  get;  set; } 

#if UNITY_EDITOR
        public void OnValidate()
        {
            if (_backToMenuButton == null)
            {
                Debug.LogError($"{gameObject.name}: _backToMenuButton is not set!", this);
            }

            if (_reloudButton == null)
            {
                Debug.LogError($"{gameObject.name}: _reloudButton is not set!", this);
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
            _backToMenuButton.onClick.AddListener(ClickBackToMenu);
            _reloudButton.onClick.AddListener(ReloudLevel);
        }

        private void OnDisable()
        {
            _backToMenuButton.onClick.RemoveListener(ClickBackToMenu);
            _reloudButton.onClick.RemoveListener(ReloudLevel);
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

        private void ClickBackToMenu()
        {
            _eventBus.Publish(new TransitToWindowEvent { });

        }

        private void ReloudLevel()
        {
            _eventBus.Publish(new TransitToPanelEvent { windowName = "GameInterfacePanel" });
        }
    }
}
