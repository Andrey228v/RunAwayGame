using Assets._Scripts.GameMVP.Language;
using Assets._Scripts.UI._1MenuWindow.Language;
using Assets.ScriptableObjects.Language;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI._1MenuWindow
{
    public enum PageName
    {
        Menu,
        Settings,
        Shop,
        Achievements
    }

    public interface ISettingsView
    {
        void UpdateSettingsDisplay();
    }

    public class MenuTabsView : MonoBehaviour, ILanguageFlip
    {
        [Header("Tabs")]
        [SerializeField] private List<GameObject> _panels;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _goldsText;
        [SerializeField] private TextMeshProUGUI _gobeletsText;
        [SerializeField] private TextMeshProUGUI _textSliderAllAudio;
        [SerializeField] private TextMeshProUGUI _textSliderMusic;
        [SerializeField] private TextMeshProUGUI _textSliderEffect;
        [SerializeField] private TextMeshProUGUI _textAudioFlipToggle;

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
        [SerializeField] private LanguageViewMenu _languageViewMenu;

        [Header("Sliders")]
        [SerializeField] private Slider _sliderAllAudio;
        [SerializeField] private Slider _sliderMusic;
        [SerializeField] private Slider _sliderEffect;

        [Header("Parents")]
        [SerializeField] private Transform _achievmentsParent;
        [SerializeField] private Transform _levelsParent;

        [Header("Toggle")]
        [SerializeField] private Toggle _audioFlipToggle;

        private GameObject _currentPanel;
        private GameObject _previousPanel;

        public Transform AchievmentsParent => _achievmentsParent;
        public Transform LevelsParent => _levelsParent;

        public event Action OnDestroyView;
        public event Action OnLanguageButtonClick;
        public event Action<int> OnLevelStart0;
        public event Action<int> OnLevelStart1;
        public event Action<int> OnLevelStart2;
        
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

        //Под вопросом...
        private void StartGame(int levelId)
        {
            if (levelId == 0)
            {
                OnLevelStart0?.Invoke(1);
            }
            else if (levelId == 1) 
            {
                OnLevelStart1?.Invoke(2);
            }
            else if(levelId == 2)
            {
                OnLevelStart2?.Invoke(3);
            }
        }

        private void ClickBackButton()
        {
            _currentPanel.SetActive(false);
            _currentPanel = _previousPanel;
            _previousPanel.SetActive(true);
        }

        private void SetupButtons()
        {
            _startGameButtonL0.onClick.AddListener(() => StartGame(0)); // переделать...
            _startGameButtonL1.onClick.AddListener(() => StartGame(1)); // переделать...
            _startGameButtonL2.onClick.AddListener(() => StartGame(2)); // переделать...

            _settingsButton.onClick.AddListener(()=> ShowPage(PageName.Settings));
            _shopButton.onClick.AddListener(() => ShowPage(PageName.Shop));
            _achievementsButton.onClick.AddListener(() => ShowPage(PageName.Achievements));

            _backButtonSetting.onClick.AddListener(ClickBackButton);
            _backButtonShop.onClick.AddListener(ClickBackButton);
            _backButtonAchievements.onClick.AddListener(ClickBackButton);

            _deletSaveButton.onClick.AddListener(DeletSave);
            _exitButton.onClick.AddListener(ClickExit);

            //_lanuageButton.onClick.AddListener(LanguageButtonClick);
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

            //_lanuageButton.onClick.RemoveListener(LanguageButtonClick);
        }

        private void ClickExit()
        {
            Application.Quit();
        }

        private void DeletSave()
        {

        }

        public void SetCoinsCountText(int actualCoin, int addCoin)
        {
            _goldsText.text  = actualCoin.ToString();
        }

        public void SetGobeletsCountText(int actualGobelets, int addGobelets)
        {
            _gobeletsText.text = actualGobelets.ToString();
        }

        public void SetLanguage(LanguageConfig languageConfig)
        {
            TextMeshProUGUI settingsButtonText = _settingsButton.GetComponentInChildren<TextMeshProUGUI>();
            TextMeshProUGUI achievmentbuttonText = _achievementsButton.GetComponentInChildren<TextMeshProUGUI>();

            if (settingsButtonText != null)
            {
                settingsButtonText.text = languageConfig.ButtonSettingName;
            }

            if (achievmentbuttonText != null)
            {
                achievmentbuttonText.text = languageConfig.ButtonAchievmentsName;
            }
        }
    }
}
