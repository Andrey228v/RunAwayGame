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
        //private LevelConfig _levelConfig;
        private LevelsController _levelsController;
        private AchievmentsController _achievmentsController;
        private ShopController _shopController;
        private WalletController _walletController;
        private EventBus _eventBus;
        private IGameLogger _gameLogger;

        public GameSaveData GameSaveData => _gameSaveData;
        //public LevelConfig Config => _levelConfig;

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
        }

        public void Start()
        {
            _gameLogger.Log("Инициализация GameSaveLoadService", "Service");


            _eventBus.Subscribe<SaveGameEvent>(OnSaveGame);
            _eventBus.Subscribe<LoadGameEvent>(OnLoad);
            _eventBus.Subscribe<DeletSaveEvent>(ResetAllProgress);

            //_eventBus.Subscribe<LevelCompletedEvent>(OnFinishLevel);
            //_eventBus.Subscribe<SaveGameEvent>(OnSaveGame);
            //_eventBus.Subscribe<LoadGameEvent>(OnLoad);
            //_eventBus.Subscribe<TransitToWindowEvent>(CloseLevel);
            //_eventBus.Subscribe<ChooseLevelEvent>(OnSetLevelConfig);
            //_eventBus.Subscribe<DeletSaveEvent>(ResetAllProgress);
            //_eventBus.Subscribe<UpdateUIEvent>(UpdateAllUI);

            //InitializeAllServices();
            LoadAllServices();

            _gameLogger.Log("GameSaveLoadService инициализирован успешно", "Service");
        }


        public void Dispose()
        {
            _gameLogger.Log("GameSaveLoadService Dispose", "Service");

            _eventBus.Unsubscribe<SaveGameEvent>(OnSaveGame);
            _eventBus.Unsubscribe<LoadGameEvent>(OnLoad);
            _eventBus.Unsubscribe<DeletSaveEvent>(ResetAllProgress);

            //_eventBus.Unsubscribe<LevelCompletedEvent>(OnFinishLevel);
            //_eventBus.Unsubscribe<SaveGameEvent>(OnSaveGame);
            //_eventBus.Unsubscribe<LoadGameEvent>(OnLoad);
            //_eventBus.Unsubscribe<TransitToWindowEvent>(CloseLevel);
            //_eventBus.Unsubscribe<ChooseLevelEvent>(OnSetLevelConfig);
            //_eventBus.Unsubscribe<DeletSaveEvent>(ResetAllProgress);
            //_eventBus.Unsubscribe<UpdateUIEvent>(UpdateAllUI);

            //_levelsController.Dispose();
            //_achievmentsController.Dispose();
            //_shopController.Dispose();
            //_walletController.Dispose();

            SaveGame();

            _gameLogger.Log("GameSaveLoadService Dispose complite", "Service");
        }


        //public void InitializeAllServices()
        //{
        //    _gameLogger.Log("GameSaveLoadService initing all services", "Service");

        //    _levelsController.Initialize(_gameSaveData, _levelConfig);
        //    _shopController.Initialize();

        //    _gameLogger.Log("GameSaveLoadService have inited all services", "Service");
        //}

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
                _levelsController.SaveAllServices(_gameSaveData, levelConfig);
            }

            SaveGame();

            _gameLogger.Log("GameSaveLoadService save all services complite", "Save");
        }

        public void LoadAllServices() 
        {
            _gameLogger.Log("GameSaveLoadService load all services", "Load");

            var levelConfig = _levelsController.Config;

            _levelsController.LoadAllServices(_gameSaveData, levelConfig);
            _achievmentsController.LoadAllServices(_gameSaveData, levelConfig);
            _shopController.LoadAllServices(_gameSaveData, levelConfig);
            _walletController.LoadAllServices(_gameSaveData);

            _gameLogger.Log("GameSaveLoadService load all services complite", "Load");
        }

        //public void UpdateAllUI(UpdateUIEvent args)
        //{
        //    _gameLogger.Log("GameSaveLoadService UpdateAllUI", "Service");
        //    _achievmentsController.UpdateView();
        //}

        //public async void OnSetLevelConfig(ChooseLevelEvent args)
        //{
        //    _levelConfig = args.levelConfig;
        //    _gameLogger.Log("GameSaveLoadService set Level Config", "Service");
        //    _levelsController.Initialize(_gameSaveData, _levelConfig);
        //    _levelsController.LoadAllServices(_gameSaveData, _levelConfig);
        //}

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

            _achievmentsController.Reset(_gameSaveData, levelConfig);
            _achievmentsController.UpdateView();

            _walletController.Reset(_gameSaveData, levelConfig);
            _walletController.UpdateView();


        }


        //public void SetLevelConfig(LevelConfig levelConfig)
        //{
        //    _levelConfig = levelConfig;
        //}

        //public void CloseLevel(TransitToWindowEvent args)
        //{
        //    _gameLogger.Log("GameSaveLoadService close level", "Service");
        //    _levelConfig = null;
        //    _levelsController.Dispose();
        //}

        //public void RestartLevel()
        //{
        //    _gameLogger.Log("GameSaveLoadService reset level", "Service");
        //    _gameSaveData.LevelsData[_levelConfig.LevelName].ResetData(_levelConfig);
        //}

        //public void OnFinishLevel(LevelCompletedEvent args) // переделать...
        //{
        //    _gameLogger.Log("GameSaveLoadService FINISH level", "Service");

        //    _levelsController.FinishLevel(_gameSaveData, _levelConfig, args);


        //    RestartLevel();
        //    SaveGame();
        //}

        //public void DieRestart()
        //{
        //    _gameLogger.Log("GameSaveLoadService Die restart", "Service");
        //    _levelsController.DieRestart(_gameSaveData, _levelConfig);
        //}

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

        private void OnLoad(LoadGameEvent args)
        {
            _gameLogger.Log("GameSaveLoadService LoadGameEvent", "Load");

            var levelConfig = _levelsController.Config;
            _levelsController.LoadAllServices(_gameSaveData, levelConfig);
        }

        public void LoadOrCreateSave()
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
