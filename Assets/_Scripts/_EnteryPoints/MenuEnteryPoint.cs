using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.GameMVP.Language;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.SceneLoading;
using Assets._Scripts.UI._1MenuWindow;
using Assets._Scripts.UI._1MenuWindow.Language;
using Assets._Scripts.Utilites.Loger;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class MenuEnteryPoint : IInitializable, IStartable, IDisposable
    {
        private List<SceneGroupHandle> _scensGroups;
        private GameSaveLoadService _gameSaveLoadService;
        private AchievmentsController _achievmentsController;
        private LevelsController _levelsController;
        private WalletController _walletController;
        private IGameLogger _gameLogger;
        private LanguageController _languageController;
        private LanguageViewMenu _viewLanguageMenu;
        private MenuTabsView _menuTabsView;
        private LoadManager _loadManager;
        private readonly LanguageManger _languageManger;

        public MenuEnteryPoint(
            AchievmentsController achievmentsController,
            IGameLogger gameLogger,
            WalletController walletController,
            GameSaveLoadService gameSaveLoadService,
            LevelsController levelsController,
            LanguageController languageController,
            MenuTabsView menuTabsView,
            LoadManager loadManager,
            LanguageViewMenu viewLanguageMenu,
            LanguageManger languageManger,
            List<SceneGroupHandle> scensGroups
            ) 
        {
            _scensGroups = scensGroups;
            _achievmentsController = achievmentsController;
            _walletController = walletController;
            _gameLogger = gameLogger;
            _levelsController = levelsController;
            _gameSaveLoadService = gameSaveLoadService;
            _languageController = languageController;
            _viewLanguageMenu = viewLanguageMenu;
            _menuTabsView = menuTabsView;
            _loadManager = loadManager;
            _languageManger = languageManger;

            //_languageController.AddMenuView("viewLanguageMenu", _viewLanguageMenu);
            _languageController.AddView(_viewLanguageMenu);
        }

        public void Initialize()
        {
            _walletController.AddMenuView(_menuTabsView);
            _achievmentsController.AddMenuView(_menuTabsView.AchievmentsParent);
            _levelsController.AddMenuView(_menuTabsView.LevelsParent);
            //_languageController.AddMenuView("viewLanguageMenu", _viewLanguageMenu);

            _languageManger.AddLangageFlip(_menuTabsView);
        }

        public void Start()
        {
            var gameSaveData = _gameSaveLoadService.GameSaveData;

            _levelsController.Initialization(gameSaveData);

            _levelsController.Load(gameSaveData);
            _walletController.Load(gameSaveData);
            _achievmentsController.Load(gameSaveData);
            _languageController.Load(gameSaveData);


            _menuTabsView.OnLevelStart0 += LoadLevel;
            _menuTabsView.OnLevelStart1 += LoadLevel;
            _menuTabsView.OnLevelStart2 += LoadLevel;

            //_languageController.AddMenuView("viewLanguageMenu", _viewLanguageMenu);

            //_loadManager.LoadScene()
        }

        public void Dispose()
        {
            _gameLogger.Log("MenuEnteryPoint OnDestroy", "Warning");
            _achievmentsController.DisposeMenuView();
            _levelsController.DisposeMenuView();

            _menuTabsView.OnLevelStart0 -= LoadLevel;
            _menuTabsView.OnLevelStart1 -= LoadLevel;
            _menuTabsView.OnLevelStart2 -= LoadLevel;

            _languageController.Dispose();

            _languageManger.RemoveLanguageFlip(_menuTabsView);

            //_languageController.RemoveMenuView("viewLanguageMenu");
        }

        //Временное решение ?? 
        private async void LoadLevel(int id)
        {
            await _loadManager.LoadScene(_scensGroups[id]);
        }

    }
}
