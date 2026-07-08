using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.SaveLoad.Data.Interfaces;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers.Levels
{
    //Основная цель сделать так, чтобы уровень не зависил от частей его наполнения и они могли быть разной конфигурации.
    // для этого всё делает интерфейсами, выделяя в них только общее и вынося уже для каждого в своё.
    // Разные типы уровней будут обладать разными LevelEntryPoin. Благодаря этому их можно наполнять по разному.
    public class LevelsController
    {
        private readonly List<IInit> _initList; // каждый подэлемент должен сам инициализировать то, что будет. 
        private readonly List<ISave> _saveList;
        private readonly List<ILoad> _loadList;
        private readonly List<IDieRestart> _restartList;
        private readonly List<IFinish> _finishList;
        private readonly List<IReset> _resetList;

        private LevelConfig _levelConfig;
        private LevelData _levelData;

        public LevelConfig Config => _levelConfig;
        public List<ISave> SaveList => _saveList;
        public List<ILoad> LoadList => _loadList;
        public List<IDieRestart> RestartList => _restartList;
        public List<IFinish> FinishList => _finishList;
        public List<IReset> ResetList => _resetList;

        public LevelsController()
        {
            _saveList = new List<ISave>();
            _loadList = new List<ILoad>();
            _restartList = new List<IDieRestart>();
            _finishList = new List<IFinish>();
            _resetList = new List<IReset>();
        }

        public void Dispose()
        {
            _saveList.Clear();
            _loadList.Clear();
            _restartList.Clear();
            _finishList.Clear();
            _resetList.Clear();
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
                        new Dictionary<string, CoinData>()
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

        public void LoadAllServices(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "levelData cannot be null");
            }

            if (_levelConfig == null)
            {
                return;
            }

            foreach (ILoad load in _loadList)
            {
                load.Load(levelData);
            }
        }

        public void DieRestart(GameSaveData gameSaveData)
        {
            foreach (IDieRestart restart in _restartList)
            {
                restart.DieRestart(_levelData);
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
        }

        public void ResetLevel(LevelData levelData, LevelConfig levelConfig)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "gameSaveData cannot be null");
            }

            foreach (IReset reset in _resetList)
            {
                reset.ResetAllObjects(levelConfig);
            }
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
                _levelData = gameSaveData.LevelsData[levelConfig.LevelName];
            }
            else
            {
                _levelData = null;
            }
        }
    }
}
