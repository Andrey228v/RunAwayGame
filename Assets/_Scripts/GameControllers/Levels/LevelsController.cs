using Assets._Scripts.SaveLoad.Data;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers.Levels
{
    public class LevelsController
    {
        private readonly List<ISave> _saveList;
        private readonly List<ILoad> _loadList;
        private readonly List<IRestart> _restartList;
        private readonly List<IFinish> _finishList;

        private LevelConfig _levelConfig;
        private LevelData _levelData;

        public LevelConfig Config => _levelConfig;
        public List<ISave> SaveList => _saveList;
        public List<ILoad> LoadList => _loadList;
        public List<IRestart> RestartList => _restartList;
        public List<IFinish> FinishList => _finishList;

        public LevelsController()
        {
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

        public void Initialization(GameSaveData gameSaveData)
        {
            if (gameSaveData == null)
            {
                throw new ArgumentNullException(nameof(gameSaveData), "gameSaveData cannot be null");
            }

            if (gameSaveData.LevelsData.TryGetValue(_levelConfig.LevelName, out LevelData levelData) == false)
            {
                LevelData newLevelData = new LevelData
                    (
                        false,
                        _levelConfig.StartPosition,
                        new PlayerData()
                        {
                            PlayerPosition = _levelConfig.StartPosition,
                            PlayerRotation = _levelConfig.PlayerStartRotation
                        },
                        new Dictionary<string, CheckPointData>(),
                        new List<CoinData>()
                    ); { };

                gameSaveData.LevelsData.Add(_levelConfig.LevelName, newLevelData);
            }
        }

        public void SaveAllServices(GameSaveData gameSaveData)
        {
            if (gameSaveData == null)
            {
                throw new ArgumentNullException(nameof(gameSaveData), "gameSaveData cannot be null");
            }

            foreach (ISave save in _saveList)
            {
                save.Save(_levelData);
            }
        }

        public void LoadAllServices(GameSaveData gameSaveData)
        {
            if (gameSaveData == null)
            {
                throw new ArgumentNullException(nameof(gameSaveData), "gameSaveData cannot be null");
            }

            if (_levelConfig == null)
            {
                return;
            }

            foreach (ILoad load in _loadList)
            {
                load.Load(_levelData);
            }
        }

        public void DieRestart(GameSaveData gameSaveData)
        {
            foreach (IRestart restart in _restartList)
            {
                restart.Restart(_levelData);
            }
        }

        public void FinishLevel(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "gameSaveData cannot be null");
            }

            foreach (IFinish finish in _finishList)
            {
                finish.Finish(_levelData);
            }

            //if (args.lvlId == "0") // переделать. Это не тут должно быть
            //{
            //    _eventBus.Publish(new FinishLevel1() { Progress = 1 });
            //}
            //else if (args.lvlId == "1")
            //{
            //    _eventBus.Publish(new FinishLevel2() { Progress = 1 });
            //}
            //else if (args.lvlId == "2")
            //{
            //    _eventBus.Publish(new FinishLevel3() { Progress = 1 });
            //}
        }

        public void SetLevelConfig(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
        }

        public void SetLevelData(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            if (gameSaveData == null)
            {
                throw new ArgumentNullException(nameof(gameSaveData), "gameSaveData cannot be null");
            }

            if (levelConfig != null)
            {
                _levelData = gameSaveData.LevelsData[_levelConfig.LevelName];
            }
            else
            {
                _levelData = null;
            }
        }
    }
}
