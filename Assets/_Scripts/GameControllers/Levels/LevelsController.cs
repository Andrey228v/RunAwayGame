using Assets._Scripts.EventBusGame;
using Assets._Scripts.Loger;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets._Scripts.SaveLoad.Data;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.GameControllers.Levels
{
    public class LevelsController : IStartable
    {
        private bool _isLevelWasStart;
        private PlayerController _playerController;
        private CoinController _coinController;
        private CheckPointsController _checkPointsController;
        private IGameLogger _gameLogger;

        public LevelsController(IGameLogger gameLogger)
        {
            _isLevelWasStart = false;
            _gameLogger = gameLogger;
        }

        public void Start()
        {
            _isLevelWasStart = true;
        }

        public void Dispose()
        {
            _playerController?.Dispose();
            _coinController?.Dispose();
            _checkPointsController?.Dispose();

            _playerController = null;
            _coinController = null;
            _checkPointsController = null;
        }

        public void Initialize(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            // под вопросом...
            //_coinController.Initialize();

        }

        //public void SetPlayerController(PlayerController playerController)
        //{
        //    _playerController = playerController;
        //    //_playerController?.LoadAllServices(gameSaveData, levelConfig);
        //}

        //public void SetCoinController(CoinController coinController) 
        //{
        //    _coinController = coinController;
        //}

        //public void SetCheckPointsController(CheckPointsController checkPointsController)
        //{
        //    _checkPointsController = checkPointsController;
        //}

        public void SaveAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {

            if (_playerController == null || _coinController == null || _checkPointsController == null)
                return;

            if (gameSaveData.LevelsData.TryGetValue(levelConfig.LevelName, out LevelData levelData))
            {
                levelData.IsLevelWasStarted = _isLevelWasStart;
            }
            else
            {
                LevelData newLevelData = new LevelData(false, 
                    levelConfig.StartPosition, 
                    new PlayerData(), 
                    new List<CheckPointData>(), 
                    new List<CoinData>()){ };

                gameSaveData.LevelsData.Add(levelConfig.LevelName, newLevelData);
            }

            _playerController.SaveAllServices(gameSaveData, levelConfig);
            _coinController.SaveAllServices(gameSaveData, levelConfig);
            _checkPointsController.SaveAllServices(gameSaveData, levelConfig);
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
                LevelData newLevelData = new LevelData(false, levelConfig.StartPosition, new PlayerData(), new List<CheckPointData>(), new List<CoinData>()) { };
                gameSaveData.LevelsData.Add(levelConfig.LevelName, levelData);
            }

            if (_playerController == null || _coinController == null || _checkPointsController == null)
            {
                _gameLogger.LogError("_plaer, _coin, _ckeck NULL");
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

        public void FinishLevel(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            _playerController.FinishGame(gameSaveData, levelConfig);
            _coinController.FinishGame();
            _checkPointsController.FinishGame();
        }
    }
}
