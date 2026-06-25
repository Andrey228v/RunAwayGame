using Assets._Scripts.EventBusGame;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.GameShop;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using VContainer.Unity;

namespace Assets._Scripts.GameControllers
{
    public class GameLoopController : IDisposable, IStartable
    {
        private LevelsController _levelsController;
        private AchievmentsController _achievmentsController;
        private ShopController _shopController;
        private WalletController _walletController;
        private EventBus _eventBus;
        private IGameLogger _gameLogger;
        private GameSaveLoadService _gameSaveLoadService;
        private LevelConfig _levelConfig;
        private GameSaveData _gameSaveData;

        public GameLoopController(LevelsController levelsController,
            AchievmentsController achievmentsController,
            ShopController shopController,
            WalletController walletController,
            EventBus eventBus,
            GameSaveLoadService gameSaveLoadService,
            IGameLogger gameLogger)
        {
            _levelsController = levelsController;
            _achievmentsController = achievmentsController;
            _shopController = shopController;
            _walletController = walletController;
            _eventBus = eventBus;
            _gameLogger = gameLogger;
            _gameSaveLoadService = gameSaveLoadService;

            _levelConfig = levelsController.Config;
            _gameSaveData = gameSaveLoadService.GameSaveData;
        }

        public void Start()
        {
            _eventBus.Subscribe<FinishLevelEvent>(OnFinishLevel);
            _eventBus.Subscribe<TransitToWindowEvent>(CloseLevel);
            _eventBus.Subscribe<UpdateUIEvent>(UpdateAllUI);
            //_eventBus.Subscribe<DieEvent>(DieRestart);

            //_eventBus.Subscribe<SaveGameEvent>(OnSaveGame);
            //_eventBus.Subscribe<LoadGameEvent>(OnLoad);
            //_eventBus.Subscribe<ChooseLevelEvent>(OnSetLevelConfig);
            //_eventBus.Subscribe<DeletSaveEvent>(ResetAllProgress);
        }

        public void Dispose()
        {
            _eventBus.Unsubscribe<FinishLevelEvent>(OnFinishLevel);
            _eventBus.Unsubscribe<TransitToWindowEvent>(CloseLevel);
            _eventBus.Unsubscribe<UpdateUIEvent>(UpdateAllUI);
            //_eventBus.Unsubscribe<DieEvent>(DieRestart);
            //_eventBus.Unsubscribe<SaveGameEvent>(OnSaveGame);
            //_eventBus.Unsubscribe<LoadGameEvent>(OnLoad);
            //_eventBus.Unsubscribe<ChooseLevelEvent>(OnSetLevelConfig);
            //_eventBus.Unsubscribe<DeletSaveEvent>(ResetAllProgress);

            _levelsController.Dispose();
            _achievmentsController.Dispose();
            _shopController.Dispose();
            _walletController.Dispose();
        }

        //public void InitializeAllServices()
        //{
        //    var gameSaveData = _gameSaveLoadService.GameSaveData;
        //    var levelConfig = _levelsController.Config;

        //    _shopController.Initialize();

        //}

        public void OnFinishLevel(FinishLevelEvent args) // переделать...
        {
            _gameLogger.Log("GameSaveLoadService FINISH level", "Service");
            var levelConfig = _levelsController.Config;

            _levelsController.FinishLevel(_gameSaveData, levelConfig, args);

            RestartLevel();
            _gameSaveLoadService.SaveGame();
        }


        public void UpdateAllUI(UpdateUIEvent args)
        {
            _gameLogger.Log("GameLoopController UpdateAllUI", "Service");
            _achievmentsController.UpdateView();
        }

        public void CloseLevel(TransitToWindowEvent args)
        {
            _gameLogger.Log("GameSaveLoadService close level", "Service");
            _levelsController.SetLevelConfig(null);
            _levelsController.Dispose();
        }

        private void RestartLevel()
        {
            _gameLogger.Log("GameSaveLoadService reset level", "Service");
            var levelConfig = _levelsController.Config;
            _gameSaveData.LevelsData[levelConfig.LevelName].ResetData(levelConfig);
        }

        public void DieRestart() 
        {
            _gameLogger.Log("DieRestart", "Service");

            var levelConfig = _levelsController.Config;

            _levelsController.DieRestart(_gameSaveData);

            RestartLevel();
            _gameSaveLoadService.SaveGame();
        }
    }
}
