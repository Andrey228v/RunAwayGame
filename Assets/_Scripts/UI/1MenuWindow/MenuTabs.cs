using Assets._Scripts.SceneLoading;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets._Scripts.UI._1MenuWindow
{
    public class MenuTabs : MonoBehaviour
    {
        [Header("Tabs")]
        [SerializeField] private GameObject _mainPagePanel;
        [SerializeField] private GameObject _settingsPanel;

        [Header("Buttons")]
        [SerializeField] private Button _startGameButtonL1;
        [SerializeField] private Button _startGameButtonL2;
        [SerializeField] private Button _startGameButtonL3;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _soundControllButton;
        [SerializeField] private Button _deletSaveButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _exitButton;

        [Header("Sliders")]
        [SerializeField] private Slider _volumeMusicSlider;

        private GameObject _currentPanel;
        private GameObject _previousPanel;
        private LoadScreenView _loadScreenView;

        [Inject]
        public void Constructor(LoadScreenView loadScreenView) // так ли... не знаю. Надо подумать...
        {
            _loadScreenView = loadScreenView;
        }

        private void OnEnable()
        {
            Show();
            _currentPanel = _mainPagePanel;
        }

        private void Start()
        {
            _startGameButtonL1.onClick.AddListener(() => StartGame(1));
            _startGameButtonL2.onClick.AddListener(() => StartGame(2));
            _startGameButtonL3.onClick.AddListener(() => StartGame(3));

            SetupButtons();
            ShowMainPage();
        }

        private void OnDestroy()
        {
            UnSetupButtons();

            _startGameButtonL1.onClick.RemoveAllListeners();
            _startGameButtonL2.onClick.RemoveAllListeners();
            _startGameButtonL3.onClick.RemoveAllListeners();
        }

        public void ShowMainPage()
        {
            _currentPanel = _mainPagePanel;
            _previousPanel = null;
            _mainPagePanel.SetActive(true);
            _settingsPanel.SetActive(false);
        }

        public void ShowSettings()
        {
            _previousPanel = _mainPagePanel;
            _currentPanel = _settingsPanel;
            _mainPagePanel.SetActive(false);
            _settingsPanel.SetActive(true);
        }

        private async void StartGame(int level)
        {
            Debug.Log("START GAME");
            Hide();
            await _loadScreenView.LoadSceneGroup(level);
        }

        private void ClickBackButton()
        {
            _currentPanel.SetActive(false);
            _previousPanel.SetActive(true);

        }

        private void SetupButtons()
        {
            //_startGameButtonL1.onClick.AddListener(StartGame);
            _settingsButton.onClick.AddListener(ShowSettings);
            _backButton.onClick.AddListener(ClickBackButton);
            _exitButton.onClick.AddListener(ClickExit);

        }

        private void UnSetupButtons()
        {
            //_startGameButtonL1.onClick.RemoveListener(StartGame);
            _settingsButton.onClick.RemoveListener(ShowSettings);
            _backButton.onClick.RemoveListener(ClickBackButton);
            _exitButton.onClick.RemoveListener(ClickExit);
        }



        private void Show()
        {
            gameObject.SetActive(true);
            //if (_canvasGroup != null)
            //    _canvasGroup.alpha = 1f;
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }

        private void ClickExit()
        {
            Application.Quit();
        }
    }
}
