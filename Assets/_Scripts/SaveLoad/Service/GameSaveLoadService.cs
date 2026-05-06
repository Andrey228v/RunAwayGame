using Assets._Scripts.EventBusGame;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.GameShop;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.Loger;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;

namespace Assets._Scripts.SaveLoad.Service
{
    public class GameSaveLoadService : IDisposable
    {
        private EasySaveSystem _saveSystem;
        private GameSaveData _gameSaveData;
        private LevelConfig _levelConfig;
        private LevelsController _levelsController;
        private AchievmentsController _achievmentsController;
        private ShopController _shopController;
        private WalletController _walletController;
        private IEventSubscriber _eventBus;
        private IGameLogger _gameLogger;

        public GameSaveData GameSaveData => _gameSaveData;

        public GameSaveLoadService(EasySaveSystem saveSystem,
            LevelsController levelsController,
            AchievmentsController achievmentsController,
            ShopController shopController,
            WalletController walletController,
            IEventSubscriber eventBus,
            IGameLogger gameLogger) 
        {
            _saveSystem = saveSystem;
            _levelsController = levelsController;
            _achievmentsController = achievmentsController;
            _shopController = shopController;
            _walletController = walletController;
            _eventBus = eventBus;
            _gameLogger = gameLogger;

            _gameLogger.Log("Инициализация GameSaveLoadService", "Service");

            _eventBus.Subscribe<LevelCompletedEvent>(OnFinishLevel);
            _eventBus.Subscribe<SaveGameEvent>(OnSaveGame);
            _eventBus.Subscribe<LoadGameEvent>(OnLoad);
            _eventBus.Subscribe<TransitToWindowEvent>(CloseLevel);
            _eventBus.Subscribe<ChooseLevelEvent>(OnSetLevelConfig);

            LoadOrCreateSave();
            InitializeAllServices();
            LoadAllServices();

            _gameLogger.Log("GameSaveLoadService инициализирован успешно", "Service");
        }

        public void Dispose()
        {
            _gameLogger.Log("GameSaveLoadService Dispose", "Service");

            _eventBus.Unsubscribe<LevelCompletedEvent>(OnFinishLevel);
            _eventBus.Unsubscribe<SaveGameEvent>(OnSaveGame);
            _eventBus.Unsubscribe<LoadGameEvent>(OnLoad);
            _eventBus.Unsubscribe<TransitToWindowEvent>(CloseLevel);
            _eventBus.Unsubscribe<ChooseLevelEvent>(OnSetLevelConfig);

            _levelsController.Dispose();
            _achievmentsController.Dispose();
            _shopController.Dispose();
            _walletController.Dispose();

            SaveGame();

            _gameLogger.Log("GameSaveLoadService Dispose complite", "Service");
        }

        public void InitializeAllServices()
        {
            _gameLogger.Log("GameSaveLoadService initing all services", "Service");

            _levelsController.Initialize();
            _achievmentsController.Initialize();
            _shopController.Initialize();
            _walletController.Initialize();

            _gameLogger.Log("GameSaveLoadService have inited all services", "Service");
        }

        public void SaveAllServices()
        {
            _gameLogger.Log("GameSaveLoadService save all services", "Save");

            _levelsController.SaveAllServices(_gameSaveData, _levelConfig);
            _achievmentsController.SaveAllServices(_gameSaveData, _levelConfig);
            _shopController.SaveAllServices(_gameSaveData, _levelConfig);
            _walletController.SaveAllServices(_gameSaveData);

            SaveGame();

            _gameLogger.Log("GameSaveLoadService save all services complite", "Save");
        }

        public void LoadAllServices() 
        {
            _gameLogger.Log("GameSaveLoadService load all services", "Load");

            _levelsController.LoadAllServices(_gameSaveData, _levelConfig);
            _achievmentsController.LoadAllServices(_gameSaveData, _levelConfig);
            _shopController.LoadAllServices(_gameSaveData, _levelConfig);
            _walletController.LoadAllServices(_gameSaveData);

            _gameLogger.Log("GameSaveLoadService load all services complite", "Load");
        }

        public void OnSetLevelConfig(ChooseLevelEvent args)
        {
            _levelConfig = args.levelConfig;
            _gameLogger.Log("GameSaveLoadService set Level Config", "Service");
        }

        public void ResetAllProgress()
        {
            _gameLogger.Log("GameSaveLoadService reset all progress", "Service");
            _saveSystem.ResetAllProgress();
            _gameSaveData = new GameSaveData();
        }

        public void CloseLevel(TransitToWindowEvent args)
        {
            _gameLogger.Log("GameSaveLoadService close level", "Service");
            _levelConfig = null;
            _levelsController.Dispose();
        }

        public void RestartLevel()
        {
            _gameLogger.Log("GameSaveLoadService reset level", "Service");
            _gameSaveData.LevelsData[_levelConfig.LevelName].ResetData(_levelConfig);
        }

        public void OnFinishLevel(LevelCompletedEvent args) // переделать...
        {
            _gameLogger.Log("GameSaveLoadService FINISH level", "Service");

            RestartLevel();

            _levelsController.FinishLevel(_gameSaveData, _levelConfig);

            SaveGame();
        }

        public void DieRestart()
        {
            _gameLogger.Log("GameSaveLoadService Die restart", "Service");
            _levelsController.DieRestart(_gameSaveData, _levelConfig);
        }

        private void SaveGame()
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

        private void OnLoad(LoadGameEvent args)
        {
            _gameLogger.Log("GameSaveLoadService LoadGameEvent", "Load");
            _levelsController.LoadAllServices(_gameSaveData, _levelConfig);
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
                _gameSaveData = new GameSaveData();
                _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _gameSaveData);
            }
        }
    }
}
