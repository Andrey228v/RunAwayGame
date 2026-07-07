using Assets._Scripts.EventBusGame;
using Assets._Scripts.SceneLoading;
using System;
using System.Collections.Generic;
using TMPro;
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

    public class MenuTabsView : MonoBehaviour
    {
        [Header("Tabs")]
        [SerializeField] private List<GameObject> _panels;

        [Header("Buttons")]
        [SerializeField] private Button _startGameButtonL0;
        [SerializeField] private Button _startGameButtonL1;
        [SerializeField] private Button _startGameButtonL2;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _soundControllButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _achievementsButton;
        [SerializeField] private Button _deletSaveButton;
        [SerializeField] private Button _backButtonSetting;
        [SerializeField] private Button _backButtonShop;
        [SerializeField] private Button _backButtonAchievements;
        [SerializeField] private Button _exitButton;

        [Header("Amounts")]
        [SerializeField] private TextMeshProUGUI _goldsText;
        [SerializeField] private TextMeshProUGUI _gobeletsText;

        [Header("Sliders")]
        [SerializeField] private Slider _volumeMusicSlider;

        [Header("Parents")]
        [SerializeField] private Transform _achievmentsParent;

        private GameObject _currentPanel;
        private GameObject _previousPanel;
        private LoadManager _loadManager;
        private List<SceneGroupHandle> _scensGroups;
        private EventBus _eventBus;

        public Transform AchievmentsParent => _achievmentsParent;

        public event Action OnDestroyView;

        [Inject]
        public void Constructor( 
            LoadManager loadManager,
            List<SceneGroupHandle> scensGroups,
            EventBus eventBus
            )
        {
            _loadManager = loadManager;
            _scensGroups = scensGroups;
            _eventBus = eventBus;
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
            OnDestroyView?.Invoke();
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
            if (level == 1) // переделать...
            {
                _eventBus.Publish(new StartLevel1 { Progress = 1});

            }
            else if (level == 2) // переделать...
            {
                _eventBus.Publish(new StartLevel2 { Progress = 1 });
            }
            else if (level == 3) // переделать...
            {
                _eventBus.Publish(new StartLevel3 { Progress = 1 });
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
            _startGameButtonL0.onClick.AddListener(() => StartGame(1)); // переделать...
            _startGameButtonL1.onClick.AddListener(() => StartGame(2)); // переделать...
            _startGameButtonL2.onClick.AddListener(() => StartGame(3)); // переделать...

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

            _startGameButtonL0.onClick.RemoveAllListeners();
            _startGameButtonL1.onClick.RemoveAllListeners();
            _startGameButtonL2.onClick.RemoveAllListeners();
        }

        private void ClickExit()
        {
            Application.Quit();
        }

        private void DeletSave()
        {
            //_eventBus.Publish(new DeletSaveEvent { });
            //_eventBus.Publish(new UpdateUIEvent { });
        }

        public void SetCoinsCountText(int actualCoin, int addCoin)
        {
            _goldsText.text  = actualCoin.ToString();
        }

        public void SetGobeletsCountText(int actualGobelets, int addGobelets)
        {
            _gobeletsText.text = actualGobelets.ToString();
        }
    }
}
