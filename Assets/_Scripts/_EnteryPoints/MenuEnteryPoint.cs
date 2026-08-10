using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.GameMVP.Language;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.UI._1MenuWindow;
using Assets._Scripts.UI._1MenuWindow.Achievements;
using Assets._Scripts.UI._1MenuWindow.Language;
using Assets._Scripts.Utilites.Loger;
using System;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class MenuEnteryPoint : IInitializable, IStartable, IDisposable
    {
        private GameSaveLoadService _gameSaveLoadService;
        private Func<MenuTabsView> _menuFactory;
        private AchievmentsController _achievmentsController;
        private LevelsController _levelsController;
        private WalletController _walletController;
        private IGameLogger _gameLogger;
        private LanguageController _languageController;
        private LanguageViewMenu _viewLanguageMenu;

        public MenuEnteryPoint(
            Func<MenuTabsView> menuFactory,
            AchievmentsController achievmentsController,
            IGameLogger gameLogger,
            WalletController walletController,
            GameSaveLoadService gameSaveLoadService,
            LevelsController levelsController,
            LanguageController languageController,
            LanguageViewMenu viewLanguageMenu
            ) 
        {
            _menuFactory = menuFactory;
            _achievmentsController = achievmentsController;
            _walletController = walletController;
            _gameLogger = gameLogger;
            _levelsController = levelsController;
            _gameSaveLoadService = gameSaveLoadService;
            _languageController = languageController;
            _viewLanguageMenu = viewLanguageMenu;
        }

        public void Initialize()
        {
            MenuTabsView menuTabsView = _menuFactory();

            _walletController.AddMenuView(menuTabsView);
            _achievmentsController.AddMenuView(menuTabsView.AchievmentsParent);
            _levelsController.AddMenuView(menuTabsView.LevelsParent);
            _languageController.AddMenuView(_viewLanguageMenu);
        }

        public void Start()
        {
            var gameSaveData = _gameSaveLoadService.GameSaveData;

            _levelsController.Initialization(gameSaveData);


            _levelsController.Load(gameSaveData);
            _walletController.Load(gameSaveData);
            _achievmentsController.Load(gameSaveData);
        }

        public void Dispose()
        {
            _gameLogger.Log("MenuEnteryPoint OnDestroy", "Warning");
            _achievmentsController.DisposeMenuView();
            _levelsController.DisposeMenuView();
        }
    }
}
