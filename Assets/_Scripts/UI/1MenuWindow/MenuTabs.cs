using Assets._Scripts.SceneLoading;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Assets._Scripts.UI._1MenuWindow
{
    public enum PageName
    {
        Menu,
        Settings,
        Shop,
        Achievements
    }


    public class MenuTabs : MonoBehaviour
    {
        [Header("Tabs")]
        [SerializeField] private List<GameObject> _panels;

        [Header("Buttons")]
        [SerializeField] private Button _startGameButtonL1;
        [SerializeField] private Button _startGameButtonL2;
        [SerializeField] private Button _startGameButtonL3;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _soundControllButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _achievementsButton;
        [SerializeField] private Button _deletSaveButton;
        [SerializeField] private Button _backButtonSetting;
        [SerializeField] private Button _backButtonShop;
        [SerializeField] private Button _backButtonAchievements;
        [SerializeField] private Button _exitButton;

        [Header("Sliders")]
        [SerializeField] private Slider _volumeMusicSlider;

        [Header("Parents")]
        [SerializeField] private Transform _achievmentsParent;


        public event Action<LevelConfig> OnChooseLevel;
        public event Action OnSaveDelet;

        private GameObject _currentPanel;
        private GameObject _previousPanel;
        private List<LevelConfig> _levelConfigs;
        private LoadManager _loadManager;
        private List<SceneGroupHandle> _scensGroups;

        public Transform AchievmentsParent => _achievmentsParent;

        [Inject]
        public void Constructor(List<LevelConfig> levelConfigs, LoadManager loadManager, List<SceneGroupHandle> scensGroups)
        {
            _levelConfigs = levelConfigs;
            _loadManager = loadManager;
            _scensGroups = scensGroups;
        }

        private void OnEnable()
        {
            _currentPanel = _panels[0];
            _previousPanel = null;
        }

        private void Start()
        {
            SetupButtons();

            //Закрыли все окна.
            for(int i = 0; i < _panels.Count; i++)
            {
                _panels[i].SetActive(false);
            }

            ShowPage(PageName.Menu);
        }

        private void OnDestroy()
        {
            UnSetupButtons();
        }

        public void ShowPage(PageName pageName)
        {
            _previousPanel = _currentPanel;

            if (pageName == PageName.Menu)
            {
                _currentPanel = _panels[0];
            }
            else if (pageName == PageName.Settings)
            {
                _currentPanel = _panels[1];
            }
            else if(pageName == PageName.Shop)
            {
                _currentPanel = _panels[2];
            }
            else if (pageName == PageName.Achievements)
            {
                _currentPanel = _panels[3];
            }
            else
            {
                throw new Exception(); // так ли ....
            }

            _previousPanel.SetActive(false);
            _currentPanel.SetActive(true);
        }

        private async void StartGame(int level)
        {
            if (level == 1)
            {
                OnChooseLevel?.Invoke(_levelConfigs[0]);
            }
            else if (level == 2)
            {
                OnChooseLevel?.Invoke(_levelConfigs[1]);
            }
            else if (level == 3)
            {
                OnChooseLevel?.Invoke(_levelConfigs[2]);
            }

            await _loadManager.LoadScene(_scensGroups[level]);
        }

        private void ClickBackButton()
        {
            _currentPanel.SetActive(false);
            _currentPanel = _previousPanel;
            _previousPanel.SetActive(true);
        }

        private void SetupButtons()
        {
            _startGameButtonL1.onClick.AddListener(() => StartGame(1));
            _startGameButtonL2.onClick.AddListener(() => StartGame(2));
            _startGameButtonL3.onClick.AddListener(() => StartGame(3));

            _settingsButton.onClick.AddListener(()=> ShowPage(PageName.Settings));
            _shopButton.onClick.AddListener(() => ShowPage(PageName.Shop));
            _achievementsButton.onClick.AddListener(() => ShowPage(PageName.Achievements));

            _backButtonSetting.onClick.AddListener(ClickBackButton);
            _backButtonShop.onClick.AddListener(ClickBackButton);
            _backButtonAchievements.onClick.AddListener(ClickBackButton);

            _deletSaveButton.onClick.AddListener(DeletSave);
            _exitButton.onClick.AddListener(ClickExit);
        }

        private void UnSetupButtons()
        {
            _settingsButton.onClick.RemoveListener(() => ShowPage(PageName.Settings));
            _shopButton.onClick.RemoveListener(() => ShowPage(PageName.Shop));
            _achievementsButton.onClick.RemoveListener(() => ShowPage(PageName.Achievements));

            _backButtonSetting.onClick.RemoveListener(ClickBackButton);
            _backButtonShop.onClick.RemoveListener(ClickBackButton);
            _backButtonAchievements.onClick.RemoveListener(ClickBackButton);

            _deletSaveButton.onClick.RemoveListener(DeletSave);
            _exitButton.onClick.RemoveListener(ClickExit);

            _startGameButtonL1.onClick.RemoveAllListeners();
            _startGameButtonL2.onClick.RemoveAllListeners();
            _startGameButtonL3.onClick.RemoveAllListeners();
        }

        private void ClickExit()
        {
            Application.Quit();
        }

        private void DeletSave()
        {
            OnSaveDelet?.Invoke();
        }
    }
}
