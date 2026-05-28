using Assets._Scripts.EventBusGame;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.GameShop;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.Loger;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.SceneLoading;
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
        private LevelConfig _levelConfig;
        private LevelsController _levelsController;
        private AchievmentsController _achievmentsController;
        private ShopController _shopController;
        private WalletController _walletController;
        private EventBus _eventBus;
        private IGameLogger _gameLogger;
        //private LoadManager _loadManager;
        //private List<SceneGroupHandle> _scensGroups;

        public GameSaveData GameSaveData => _gameSaveData;
        public LevelConfig levelConfig => _levelConfig;

        public GameSaveLoadService(EasySaveSystem saveSystem,
            LevelsController levelsController,
            AchievmentsController achievmentsController,
            ShopController shopController,
            WalletController walletController,
            //LoadManager loadManager,
            //List<SceneGroupHandle> scensGroups,
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
            //_loadManager = loadManager;
            //_scensGroups = scensGroups;


        }

        public void Dispose()
        {
            _gameLogger.Log("GameSaveLoadService Dispose", "Service");

            _eventBus.Unsubscribe<LevelCompletedEvent>(OnFinishLevel);
            _eventBus.Unsubscribe<SaveGameEvent>(OnSaveGame);
            _eventBus.Unsubscribe<LoadGameEvent>(OnLoad);
            _eventBus.Unsubscribe<TransitToWindowEvent>(CloseLevel);
            _eventBus.Unsubscribe<ChooseLevelEvent>(OnSetLevelConfig);
            _eventBus.Unsubscribe<DeletSaveEvent>(ResetAllProgress);
            _eventBus.Unsubscribe<UpdateUIEvent>(UpdateAllUI);

            _levelsController.Dispose();
            _achievmentsController.Dispose();
            _shopController.Dispose();
            _walletController.Dispose();

            SaveGame();

            _gameLogger.Log("GameSaveLoadService Dispose complite", "Service");
        }

        public void Start()
        {
            _gameLogger.Log("Инициализация GameSaveLoadService", "Service");

            _eventBus.Subscribe<LevelCompletedEvent>(OnFinishLevel);
            _eventBus.Subscribe<SaveGameEvent>(OnSaveGame);
            _eventBus.Subscribe<LoadGameEvent>(OnLoad);
            _eventBus.Subscribe<TransitToWindowEvent>(CloseLevel);
            _eventBus.Subscribe<ChooseLevelEvent>(OnSetLevelConfig);
            _eventBus.Subscribe<DeletSaveEvent>(ResetAllProgress);
            _eventBus.Subscribe<UpdateUIEvent>(UpdateAllUI);

            InitializeAllServices();
            LoadAllServices();

            _gameLogger.Log("GameSaveLoadService инициализирован успешно", "Service");
        }

        public void InitializeAllServices()
        {
            _gameLogger.Log("GameSaveLoadService initing all services", "Service");

            LoadOrCreateSave();

            _levelsController.Initialize(_gameSaveData, _levelConfig);
            _achievmentsController.Initialize(_gameSaveData, _levelConfig);
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

        public void UpdateAllUI(UpdateUIEvent args)
        {
            _gameLogger.Log("GameSaveLoadService UpdateAllUI", "Service");
            //_achievmentsController.UpdateUI(_gameSaveData, _levelConfig);
        }

        public async void OnSetLevelConfig(ChooseLevelEvent args)
        {
            _levelConfig = args.levelConfig;
            _gameLogger.Log("GameSaveLoadService set Level Config", "Service");
            _levelsController.Initialize(_gameSaveData, _levelConfig);

            //await _loadManager.LoadScene(_scensGroups[_levelConfig.LevelId]);

            //InitializeLevel();
        }

        public void ResetAllProgress(DeletSaveEvent args)
        {
            _gameLogger.Log("GameSaveLoadService reset all progress", "Service");
            _saveSystem.ResetAllProgress();
            _gameSaveData = new GameSaveData(new Dictionary<string,
                LevelData>(),
                new List<AchievmentData>(),
                new ShopData(),
                new WalletData(),
                DateTime.Now){ };

            //обновить UI

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

            _levelsController.FinishLevel(_gameSaveData, _levelConfig, args);

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
