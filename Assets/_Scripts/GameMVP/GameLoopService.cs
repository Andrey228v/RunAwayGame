using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameMVP;
using Assets._Scripts.SaveLoad.Data.Interfaces.Game;
using Assets._Scripts.SaveLoad.Service;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers
{
    public class GameLoopService : IDisposable
    {
        private GameSaveLoadService _gameSaveLoadService;
        private LevelLoopService _levelLoopSerivece;
        private LevelsController _levelsController;

        private readonly Dictionary<string, ISaveGame> _saveDict;
        private readonly Dictionary<string, ILoadGame> _loadDict;
        private readonly Dictionary<string, IDieRestartGame> _dieRestartDict;
        private readonly Dictionary<string, IFinishGame> _finishDict;
        private readonly Dictionary<string, IResetGame> _resetDict;

        public Dictionary<string, ISaveGame> SaveDict => _saveDict;
        public Dictionary<string, ILoadGame> LoadDict => _loadDict;
        public Dictionary<string, IDieRestartGame> DieRestartDict => _dieRestartDict;
        public Dictionary<string, IFinishGame> FinishDict => _finishDict;
        public Dictionary<string, IResetGame> ResetDict => _resetDict;

        public GameLoopService(GameSaveLoadService gameSaveLoadService,
            LevelLoopService levelLoopService, LevelsController levelsController)
        {
            _gameSaveLoadService = gameSaveLoadService;
            _levelLoopSerivece = levelLoopService;
            _levelsController = levelsController;

            _saveDict = new Dictionary<string, ISaveGame>();
            _loadDict = new Dictionary<string, ILoadGame>();
            _dieRestartDict = new Dictionary<string, IDieRestartGame>();
            _finishDict = new Dictionary<string, IFinishGame>();
            _resetDict = new Dictionary<string, IResetGame>();
        }

        public void Dispose()
        {
            _saveDict.Clear();
            _loadDict.Clear();
            _dieRestartDict.Clear();
            _finishDict.Clear();
            _resetDict.Clear();
        }

        public void SaveAllServices(GameSaveData gameSaveData)
        {
            foreach (var key in _saveDict.Keys) 
            {
                _saveDict[key].Save(gameSaveData);
            }

            if(_levelsController.Config != null)
            {
                var levelName = _levelsController.Config.LevelName;
                var levelData = gameSaveData.LevelsData[levelName];
                _levelLoopSerivece.SaveAllServices(levelData);
            }

            _gameSaveLoadService.SaveGame();
        }

        public void LoadAllServices(GameSaveData gameSaveData)
        {
            foreach (var key in _loadDict.Keys)
            {
                _loadDict[key].Load(gameSaveData);
            }

            if (_levelsController.Config != null)
            {
                var levelName = _levelsController.Config.LevelName;
                var levelData = gameSaveData.LevelsData[levelName];
                _levelLoopSerivece.LoadAllServices(levelData);
            }
        }

        public void DieRestart(GameSaveData gameSaveData)
        {
            foreach (var key in _dieRestartDict.Keys)
            {
                _dieRestartDict[key].DieRestart(gameSaveData);
            }

            if (_levelsController.Config != null)
            {
                var levelName = _levelsController.Config.LevelName;
                var levelData = gameSaveData.LevelsData[levelName];
                _levelLoopSerivece.DieRestart(levelData);
            }
        }

        public void FinishLevel(GameSaveData gameSaveData)
        {
            foreach (var key in _finishDict.Keys)
            {
                _finishDict[key].Finish(gameSaveData);
            }

            if (_levelsController.Config != null)
            {
                var levelName = _levelsController.Config.LevelName;
                var levelData = gameSaveData.LevelsData[levelName];
                _levelLoopSerivece.FinishLevel(levelData);
            }
        }

        public void ResetLevel(GameSaveData gameSaveData)
        {
            foreach (var key in _finishDict.Keys)
            {
                _resetDict[key].Reset(gameSaveData);
            }

            if (_levelsController.Config != null)
            {
                var levelName = _levelsController.Config.LevelName;
                var levelData = gameSaveData.LevelsData[levelName];
                _levelLoopSerivece.ResetLevel(_levelsController.Config);
            }
        }
    }
}
