using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.GameShop;
using Assets._Scripts.GameControllers.Levels;
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

        public GameSaveData GameSaveData => _gameSaveData;

        public GameSaveLoadService(EasySaveSystem saveSystem,
            LevelsController levelsController,
            AchievmentsController achievmentsController,
            ShopController shopController) 
        {
            _saveSystem = saveSystem;
            _levelsController = levelsController;
            _achievmentsController = achievmentsController;
            _shopController = shopController;

            LoadOrCreateSave();
            InitializeAllServices();
            LoadAllServices();
        }

        public void Dispose()
        {
            _levelsController.Dispose();
            _achievmentsController.Dispose();
            _shopController.Dispose();

            SaveGame();
        }

        public void InitializeAllServices()
        {
            _levelsController.Initialize();
            _achievmentsController.Initialize();
            _shopController.Initialize();
        }

        public void SaveAllServices()
        {
            _levelsController.SaveAllServices(_gameSaveData, _levelConfig);
            _achievmentsController.SaveAllServices(_gameSaveData, _levelConfig);
            _shopController.SaveAllServices(_gameSaveData, _levelConfig);

            SaveGame();
        }

        public void LoadAllServices() 
        {
            _levelsController.LoadAllServices(_gameSaveData, _levelConfig);
            _achievmentsController.LoadAllServices(_gameSaveData, _levelConfig);
            _shopController.LoadAllServices(_gameSaveData, _levelConfig);
        }

        public void SetLevelConfig(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public void ResetAllProgress()
        {
            _saveSystem.ResetAllProgress();
            _gameSaveData = new GameSaveData();
        }

        public void CloseLevel()
        {
            _levelConfig = null;
            _levelsController.Dispose();
        }

        public void RestartLevel()
        {
            _gameSaveData = new GameSaveData();
        }

        public void FinishLevel()
        {
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
