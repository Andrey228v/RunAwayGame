using Assets._Scripts.EventBusGame;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.GameControllers.Levels
{
    public class LevelsController
    {
        private readonly IGameLogger _gameLogger;
        private readonly EventBus _eventBus;

        private readonly List<ISave> _saveList;
        private readonly List<ILoad> _loadList;
        private readonly List<IRestart> _restartList;
        private readonly List<IFinish> _finishList;

        private LevelConfig _levelConfig;

        public LevelConfig Config => _levelConfig;

        public LevelsController(IGameLogger gameLogger, EventBus eventBus)
        {
            _gameLogger = gameLogger;
            _eventBus = eventBus;

            _saveList = new List<ISave>();
            _loadList = new List<ILoad>();
            _restartList = new List<IRestart>();
            _finishList = new List<IFinish>();
        }

        public void Dispose()
        {
            _saveList.Clear();
            _loadList.Clear();
            _restartList.Clear();
            _finishList.Clear();
        }

        public void SaveAllServices(GameSaveData gameSaveData)
        {

            if (gameSaveData.LevelsData.TryGetValue(_levelConfig.LevelName, out LevelData levelData) == false)
            {
                LevelData newLevelData = new LevelData(false, 
                    _levelConfig.StartPosition,
                    new PlayerData(),
                    new List<CheckPointData>(),
                    new List<CoinData>()){ };

                gameSaveData.LevelsData.Add(_levelConfig.LevelName, newLevelData);
            }

            foreach (ISave save in _saveList)
            {
                save.Save(gameSaveData, _levelConfig);
            }
        }

        public void LoadAllServices(GameSaveData gameSaveData)
        {
            if(_levelConfig == null)
            {
                return;
            }

            if (gameSaveData.LevelsData.TryGetValue(_levelConfig.LevelName, out LevelData levelData) == false)
            {
                LevelData newLevelData = new LevelData(false, _levelConfig.StartPosition, new PlayerData(), new List<CheckPointData>(), new List<CoinData>()) { };
                gameSaveData.LevelsData.Add(_levelConfig.LevelName, newLevelData);
            }

            foreach (ILoad load in _loadList)
            {
                load.Load(gameSaveData, _levelConfig);
            }
        }

        public void DieRestart(GameSaveData gameSaveData)
        {
            var LevelData = gameSaveData.LevelsData[_levelConfig.LevelName];

            foreach (IRestart restart in _restartList)
            {
                restart.Restart(LevelData);
            }
        }

        public void FinishLevel(GameSaveData gameSaveData, FinishLevelEvent args)
        {
            foreach (IFinish finish in _finishList)
            {
                finish.Finish(gameSaveData, _levelConfig);
            }

            if (args.lvlId == "0") // переделать. Это не тут должно быть
            {
                _eventBus.Publish(new FinishLevel1() { Progress = 1 });
            }
            else if (args.lvlId == "1")
            {
                _eventBus.Publish(new FinishLevel2() { Progress = 1 });
            }
            else if (args.lvlId == "2")
            {
                _eventBus.Publish(new FinishLevel3() { Progress = 1 });
            }
        }

        public void AddSave(ISave save)
        {
            _saveList.Add(save);
        }

        public void AddLoad(ILoad load)
        {
            _loadList.Add(load);
        }

        public void AddRestart(IRestart restart)
        {
            _restartList.Add(restart);
        }

        public void AddFinish(IFinish finish)
        {
            _finishList.Add(finish);
        }

        public void SetLevelConfig(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }
    }
}
