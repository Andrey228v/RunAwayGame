using Assets._Scripts.EventBusGame;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.GameShop;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.SaveLoad.Service
{
    public class GameSaveLoadService : IDisposable, IStartable
    {
        private EasySaveSystem _saveSystem;
        private GameSaveData _gameSaveData;
        private LevelsController _levelsController;
        private AchievmentsController _achievmentsController;
        private ShopController _shopController;
        private WalletController _walletController;
        private EventBus _eventBus;
        private IGameLogger _gameLogger;

        public GameSaveData GameSaveData => _gameSaveData;

        public GameSaveLoadService(EasySaveSystem saveSystem,
            LevelsController levelsController,
            AchievmentsController achievmentsController,
            ShopController shopController,
            WalletController walletController,
            EventBus eventBus,
            IGameLogger gameLogger) 
        {
            _saveSystem = saveSystem;
            _levelsController = levelsController;
            _achievmentsController = achievmentsController;
            _shopController = shopController;
            _walletController = walletController;
            _eventBus = eventBus;
            _gameLogger = gameLogger;

            LoadOrCreateSave();
        }

        public void Start()
        {
            _gameLogger.Log("Инициализация GameSaveLoadService", "Service");

            _eventBus.Subscribe<SaveGameEvent>(OnSaveGame);
            _eventBus.Subscribe<DeletSaveEvent>(ResetAllProgress);

            LoadAllServices();

            _gameLogger.Log("GameSaveLoadService инициализирован успешно", "Service");
        }


        public void Dispose()
        {
            _gameLogger.Log("GameSaveLoadService Dispose", "Service");

            _eventBus.Unsubscribe<SaveGameEvent>(OnSaveGame);
            _eventBus.Unsubscribe<DeletSaveEvent>(ResetAllProgress);

            SaveGame();

            _gameLogger.Log("GameSaveLoadService Dispose complite", "Service");
        }

        public void SaveAllServices()
        {
            _gameLogger.Log("GameSaveLoadService save all services", "Save");

            var levelConfig = _levelsController.Config;

            if(levelConfig == null) // если конфиг null значит мы не в уровне
            {
                _achievmentsController.SaveAllServices(_gameSaveData);
                _shopController.SaveAllServices(_gameSaveData);
                _walletController.SaveAllServices(_gameSaveData);
            }
            else // мы в уровне. Сохраняем всё. 
            {
                _achievmentsController.SaveAllServices(_gameSaveData);
                _shopController.SaveAllServices(_gameSaveData);
                _walletController.SaveAllServices(_gameSaveData);
                _levelsController.SaveAllServices(_gameSaveData);
            }

            SaveGame();

            _gameLogger.Log("GameSaveLoadService save all services complite", "Save");
        }

        public void LoadAllServices() 
        {
            _gameLogger.Log("GameSaveLoadService load all services", "Load");

            var levelConfig = _levelsController.Config;

            _achievmentsController.LoadAllServices(_gameSaveData.AchievmentsData);
            _shopController.LoadAllServices(_gameSaveData, levelConfig);
            _walletController.LoadAllServices(_gameSaveData);

            _gameLogger.Log("GameSaveLoadService load all services complite", "Load");
        }

        public void ResetAllProgress(DeletSaveEvent args)
        {
            _gameLogger.Log("GameSaveLoadService reset all progress", "Service");
            _saveSystem.ResetAllProgress();

            var levelConfig = _levelsController.Config;

            _gameSaveData = new GameSaveData(
                new Dictionary<string, LevelData>(),
                new List<AchievmentData>(),
                new ShopData(),
                new WalletData(),
                DateTime.Now){ };

            //_achievmentsController.Reset(_gameSaveData, levelConfig);
            //_walletController.Reset(_gameSaveData);
        }

        public void SaveGame()
        {
            _gameLogger.Log("GameSaveLoadService Save game", "Save");
            _gameSaveData.LastSaveTime = DateTime.Now;
            _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _gameSaveData);
        }

        private void OnSaveGame(SaveGameEvent args)
        {
            _gameLogger.Log("GameSaveLoadService OnSaveGame", "Save");
            SaveAllServices();
        }

        private void LoadOrCreateSave()
        {
            _gameLogger.Log("GameSaveLoadService LoadOrCreateSave", "Service");

            if (_saveSystem.HasKey(SaveUtilites.GAME_SAVE_KEY))
            {
                _gameSaveData = _saveSystem.Load<GameSaveData>(SaveUtilites.GAME_SAVE_KEY);
            }
            else
            {
                _gameSaveData = new GameSaveData(new Dictionary<string,
                    LevelData>(),
                    new List<AchievmentData>(),
                    new ShopData(),
                    new WalletData(),
                    DateTime.Now){ };

                _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _gameSaveData);
            }
        }
    }
}
