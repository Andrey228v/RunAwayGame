using Assets._Scripts.SceneLoading;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets._Scripts.UI._1MenuWindow
{
    public class MenuTabs : MonoBehaviour
    {
        [Header("Панели вкладок")]
        [SerializeField] private GameObject _mainPagePanel;
        [SerializeField] private GameObject _settingsPanel;

        [Header("Кнопки (опционально)")]
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _exitButton;

        private GameObject _currentPanel;
        private GameObject _previousPanel;
        private LoadScreenView _loadScreenView;

        [Inject]
        public void Constructor(LoadScreenView loadScreenView) // так ли... не знаю. Надо подумать...
        {
            _loadScreenView = loadScreenView;
        }

        private void Start()
        {
            SetupButtons();
            ShowMainPage();
        }


        private void OnEnable()
        {
            Show();
            _currentPanel = _mainPagePanel;
        }

        private void OnDestroy()
        {
            UnSetupButtons();
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

        private async void StartGame()
        {
            Debug.Log("START GAME");
            Hide();
            await _loadScreenView.LoadSceneGroup(1);
        }

        private void ClickBackButton()
        {
            _currentPanel.SetActive(false);
            _previousPanel.SetActive(true);

        }

        private void SetupButtons()
        {
            _startGameButton.onClick.AddListener(StartGame);
            _settingsButton.onClick.AddListener(ShowSettings);
            _backButton.onClick.AddListener(ClickBackButton);
            _exitButton.onClick.AddListener(ClickExit);

        }

        private void UnSetupButtons()
        {
            _startGameButton.onClick.RemoveListener(StartGame);
            _settingsButton.onClick.RemoveListener(ShowSettings);
            _backButton.onClick.RemoveListener(ClickBackButton);
            _exitButton.onClick.RemoveListener(ClickExit);
        }

        private void ClickExit()
        {

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
    }
}
