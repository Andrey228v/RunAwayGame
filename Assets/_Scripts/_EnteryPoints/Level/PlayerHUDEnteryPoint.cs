using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.ObjectsScripts.UI.GamePanel;
using Assets._Scripts.SceneLoading;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class PlayerHUDEnteryPoint : IStartable, IDisposable
    {
        private Func<GamePanelView> _gamePanelFactory;
        private WalletController _walletController;
        private GamePanelController _gamePanelController;
        private GameLoopService _gameLoopService;
        private LoadManager _loadManager;
        private LevelsController _levelsController;
        private List<SceneGroupHandle> _sceneGroupHandle;
        private GamePanelView _panel;

        public PlayerHUDEnteryPoint(Func<GamePanelView> gamePanelFactory,
            GamePanelController gamePanelController,
            GameLoopService gameLoopService,
            LoadManager loadManager,
            LevelsController levelsController,
            List<SceneGroupHandle> sceneGroupHandle,
            WalletController walletController) 
        {
            _walletController = walletController;
            _gamePanelFactory = gamePanelFactory;
            _gamePanelController = gamePanelController;
            _gameLoopService = gameLoopService;
            _loadManager = loadManager;
            _levelsController = levelsController;
            _sceneGroupHandle = sceneGroupHandle;
        }

        public void Start()
        {
            _panel = _gamePanelFactory();
            _walletController.AddGamePanelView(_panel);

            _gamePanelController.AddGamePaneView(_panel);

            //_levelsController.SaveList.Add(_gamePanelController);
            //_levelsController.LoadList.Add(_gamePanelController);
            //_levelsController.RestartList.Add(_gamePanelController);
            //_levelsController.FinishList.Add(_gamePanelController);

            _panel.GameWinPanelView.OnButtonBackToMenuClick += TransitToMenuWindow;
            _panel.GameMenuPanelView.OnButtonBackToMenuClick += TransitToMenuWindow;
        }

        public void Dispose()
        {
            _panel.GameWinPanelView.OnButtonBackToMenuClick -= TransitToMenuWindow;
            _panel.GameMenuPanelView.OnButtonBackToMenuClick -= TransitToMenuWindow;
        }

        private async void TransitToMenuWindow()
        {
            await _loadManager.LoadScene(_sceneGroupHandle[0]);
        }
    }
}
