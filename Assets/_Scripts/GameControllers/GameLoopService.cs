using Assets._Scripts.EventBusGame;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.GameShop;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.ObjectsScripts.Points.Finish;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad.Data;
using System;
using VContainer.Unity;
using static UnityEngine.Rendering.GPUSort;

namespace Assets._Scripts.GameControllers
{
    public class GameLoopService : IDisposable, IStartable
    {
        private LevelsController _levelsController;
        private AchievmentsController _achievmentsController;
        private ShopController _shopController;
        private WalletController _walletController;
        private EventBus _eventBus;
        private IGameLogger _gameLogger;
        private GameSaveLoadService _gameSaveLoadService;
        private GameSaveData _gameSaveData;

        public GameLoopService(LevelsController levelsController,
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
            _gameSaveData = gameSaveLoadService.GameSaveData;
        }

        public void Start()
        {
            //_eventBus.Subscribe<FinishLevelEvent>(OnFinishLevel);
            _eventBus.Subscribe<TransitToWindowEvent>(CloseLevel);
        }

        public void Dispose()
        {
            //_eventBus.Unsubscribe<FinishLevelEvent>(OnFinishLevel);
            _eventBus.Unsubscribe<TransitToWindowEvent>(CloseLevel);

            _achievmentsController.Dispose();
            _shopController.Dispose();
            _walletController.Dispose();
        }

        public void FinishLevel()
        {

            _gameLogger.Log("GameSaveLoadService FINISH level", "Service");

            RestartLevel();

            var levelData = _gameSaveData.LevelsData[_levelsController.Config.LevelName];

            _levelsController.FinishLevel(levelData);
            _gameSaveLoadService.SaveGame();

        }


        //public void OnFinishLevel(FinishLevelEvent args) // переделать...
        //{
        //    _gameLogger.Log("GameSaveLoadService FINISH level", "Service");

        //    RestartLevel();
        //    _levelsController.FinishLevel(_gameSaveData, args);
        //    _gameSaveLoadService.SaveGame();
        //}


        //public void UpdateAllUI(UpdateUIEvent args)
        //{
        //    _gameLogger.Log("GameLoopController UpdateAllUI", "Service");
        //    //_achievmentsController.UpdateView();
        //}

        public void CloseLevel(TransitToWindowEvent args)
        {
            _gameLogger.Log("GameSaveLoadService close level", "Service");
            _levelsController.SetLevelConfig(null);
            _levelsController.SetLevelData(_gameSaveLoadService.GameSaveData, null);
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
            _gameSaveLoadService.SaveGame();
        }
    }
}
