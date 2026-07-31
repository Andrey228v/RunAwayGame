using Assets._Scripts.SaveLoad.Data.Interfaces;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameMVP
{
    public class LevelLoopService
    {
        private readonly Dictionary<string, ISave> _saveDict;
        private readonly Dictionary<string, ILoad> _loadDict;
        private readonly Dictionary<string, IDieRestart> _dieRestartDict;
        private readonly Dictionary<string, IFinish> _finishDict;
        private readonly Dictionary<string, IReset> _resetDict;

        public Dictionary<string, ISave> SaveDict => _saveDict;
        public Dictionary<string, ILoad> LoadDict => _loadDict;
        public Dictionary<string, IDieRestart> DieRestartDict => _dieRestartDict;
        public Dictionary<string, IFinish> FinishDict => _finishDict;
        public Dictionary<string, IReset> ResetDict => _resetDict;

        public LevelLoopService()
        {
            _saveDict = new Dictionary<string, ISave>();
            _loadDict = new Dictionary<string, ILoad>();
            _dieRestartDict = new Dictionary<string, IDieRestart>();
            _finishDict = new Dictionary<string, IFinish>();
            _resetDict = new Dictionary<string, IReset>();
        }

        public void Dispose()
        {
            _saveDict.Clear();
            _loadDict.Clear();
            _dieRestartDict.Clear();
            _finishDict.Clear();
            _resetDict.Clear();
        }

        public void SaveAllServices(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "levelData cannot be null");
            }

            foreach (var key in _saveDict.Keys)
            {
                _saveDict[key].Save(levelData);
            }
        }

        public void LoadAllServices(LevelData levelData)
        {
            foreach (var key in _loadDict.Keys)
            {
                _loadDict[key].Load(levelData);
            }
        }

        public void DieRestart(LevelData levelData)
        {
            foreach (var key in _dieRestartDict.Keys)
            {
                _dieRestartDict[key].DieRestart(levelData);
            }
        }

        public void FinishLevel(LevelData levelData)
        {
            foreach (var key in _finishDict.Keys)
            {
                _finishDict[key].Finish(levelData);
            }
        }

        public void ResetLevel(LevelConfig levelConfig)
        {
            foreach (var key in _finishDict.Keys)
            {
                _resetDict[key].ResetAllObjects(levelConfig);
            }
        }
    }
}
