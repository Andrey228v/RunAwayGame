using Assets._Scripts.ObjectsScripts.Coins;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.GameControllers.Levels
{
    public class LevelsController : IStartable, IDisposable //, ISaveLoadService
    {
        private bool _isLevelWasStart;
        private PlayerController _playerController;
        private CoinController _coinController;
        private CheckPointsController _checkPointsController;

        public LevelsController()
        {
            _isLevelWasStart = false;
        }

        public void Start()
        {
            _isLevelWasStart = true;
        }

        public void Dispose()
        {

        }

        public void Initialize()
        {
            // под вопросом...
        }

        public void SetPlayerController(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public void SetCoinController(CoinController coinController) 
        {
            _coinController = coinController;
        }

        public void SetCheckPointsController(CheckPointsController checkPointsController)
        {
            _checkPointsController = checkPointsController;
        }

        public void SaveAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            _playerController.SaveAllServices(gameSaveData, levelConfig);
            _coinController.SaveAllServices(gameSaveData, levelConfig);
            _checkPointsController.SaveAllServices(gameSaveData, levelConfig);

            if (gameSaveData.LevelsData.TryGetValue(levelConfig.LevelName, out LevelData levelData))
            {
                levelData.IsLevelWasStarted = _isLevelWasStart;
            }
            else
            {
                LevelData newLevelData = new LevelData { };
                gameSaveData.LevelsData.Add(levelConfig.LevelName, newLevelData);
            }
        }

        public void LoadAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            if(levelConfig == null)
            {
                return;
            }

            var levelsData = gameSaveData.LevelsData;

            if (gameSaveData.LevelsData.TryGetValue(levelConfig.LevelName, out LevelData levelData) == false)
            {
                levelData = new LevelData { };
                gameSaveData.LevelsData.Add(levelConfig.LevelName, levelData);
            }

            _playerController?.LoadAllServices(gameSaveData, levelConfig);
            _coinController?.LoadAllServices(gameSaveData, levelConfig);
            _checkPointsController?.LoadAllServices(gameSaveData, levelConfig);
        }

        public void DieRestart(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            var LevelData = gameSaveData.LevelsData[levelConfig.LevelName];

            _playerController.DieRestart(LevelData);
        }

        public void LoadLevel()
        {

        }
    }
}
