using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.GameShop;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad.Data;
using System;

namespace Assets._Scripts.GameControllers
{
    public class GameLoopService : IDisposable
    {
        private LevelsController _levelsController;
        private AchievmentsController _achievmentsController;
        private ShopController _shopController;
        private WalletController _walletController;
        private IGameLogger _gameLogger;
        private GameSaveLoadService _gameSaveLoadService;
        private GameSaveData _gameSaveData;

        public GameLoopService(LevelsController levelsController,
            AchievmentsController achievmentsController,
            ShopController shopController,
            WalletController walletController,
            GameSaveLoadService gameSaveLoadService,
            IGameLogger gameLogger)
        {
            _levelsController = levelsController;
            _achievmentsController = achievmentsController;
            _shopController = shopController;
            _walletController = walletController;
            _gameLogger = gameLogger;
            _gameSaveLoadService = gameSaveLoadService;
            _gameSaveData = gameSaveLoadService.GameSaveData;
        }

        public void Dispose()
        {
            _achievmentsController.Dispose();
            _shopController.Dispose();
            _walletController.Dispose();
        }

        public void FinishLevel(bool isActivate)
        {
            if(isActivate == true)
            {
                _gameLogger.Log("GameSaveLoadService FINISH level", "Service");

                RestartLevel();

                var levelData = _gameSaveData.LevelsData[_levelsController.Config.LevelName];

                _levelsController.FinishLevel(levelData);

                _gameSaveLoadService.SaveGame();
            }
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

            _levelsController.DieRestart(_gameSaveData);
            _gameSaveLoadService.SaveGame();
        }
    }
}
