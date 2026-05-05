using Assets._Scripts.EventBusGame;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.GameShop;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
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

        public GameSaveData GameSaveData => _gameSaveData;

        public GameSaveLoadService(EasySaveSystem saveSystem,
            LevelsController levelsController,
            AchievmentsController achievmentsController,
            ShopController shopController,
            WalletController walletController,
            IEventSubscriber eventBus) 
        {
            _saveSystem = saveSystem;
            _levelsController = levelsController;
            _achievmentsController = achievmentsController;
            _shopController = shopController;
            _walletController = walletController;

            _eventBus = eventBus;

            _eventBus.Subscribe<LevelCompletedEvent>(OnFinishLevel);
            _eventBus.Subscribe<SaveGameEvent>(OnSaveGame);
            _eventBus.Subscribe<LoadGameEvent>(OnLoad);
            _eventBus.Subscribe<TransitToWindowEvent>(CloseLevel);
            _eventBus.Subscribe<ChooseLevelEvent>(OnSetLevelConfig);

            LoadOrCreateSave();
            InitializeAllServices();
            LoadAllServices();
        }

        public void Dispose()
        {
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
        }

        public void InitializeAllServices()
        {
            _levelsController.Initialize();
            _achievmentsController.Initialize();
            _shopController.Initialize();
            _walletController.Initialize();
        }

        public void SaveAllServices()
        {
            _levelsController.SaveAllServices(_gameSaveData, _levelConfig);
            _achievmentsController.SaveAllServices(_gameSaveData, _levelConfig);
            _shopController.SaveAllServices(_gameSaveData, _levelConfig);
            _walletController.SaveAllServices(_gameSaveData, _levelConfig);

            SaveGame();
        }

        public void LoadAllServices() 
        {
            _levelsController.LoadAllServices(_gameSaveData, _levelConfig);
            _achievmentsController.LoadAllServices(_gameSaveData, _levelConfig);
            _shopController.LoadAllServices(_gameSaveData, _levelConfig);
            _walletController.LoadAllServices(_gameSaveData, _levelConfig);
        }

        public void OnSetLevelConfig(ChooseLevelEvent args)
        {
            _levelConfig = args.levelConfig;
        }

        public void ResetAllProgress()
        {
            _saveSystem.ResetAllProgress();
            _gameSaveData = new GameSaveData();
        }

        public void CloseLevel(TransitToWindowEvent args)
        {
            _levelConfig = null;
            _levelsController.Dispose();
        }

        public void RestartLevel()
        {

            //_gameSaveData = new GameSaveData();
            _gameSaveData.LevelsData[_levelConfig.LevelName].ResetData(_levelConfig);

        }

        public void OnFinishLevel(LevelCompletedEvent args) // переделать...
        {
            RestartLevel();

            _levelsController.FinishLevel(_gameSaveData, _levelConfig);

            SaveGame();
        }

        public void DieRestart()
        {
            _levelsController.DieRestart(_gameSaveData, _levelConfig);
        }

        private void SaveGame()
        {
            _gameSaveData.LastSaveTime = DateTime.Now;
            _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _gameSaveData);
        }

        private void OnSaveGame(SaveGameEvent args)
        {
            SaveAllServices();
            //SaveGame();
        }

        private void OnLoad(LoadGameEvent args)
        {
            _levelsController.LoadAllServices(_gameSaveData, _levelConfig);
        }

        private void LoadOrCreateSave()
        {
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
