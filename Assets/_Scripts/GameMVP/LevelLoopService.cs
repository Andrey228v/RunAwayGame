using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.SaveLoad.Data.Interfaces;
using Assets._Scripts.SaveLoad.Data.Interfaces.Game;
using Assets._Scripts.SaveLoad.Service;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.GameMVP
{
    public class LevelLoopService
    {
        private readonly Dictionary<string, IInit> _initDict; // каждый подэлемент должен сам инициализировать то, что будет. 
        private readonly Dictionary<string, ISave> _saveDict;
        private readonly Dictionary<string, ILoad> _loadDict;
        private readonly Dictionary<string, IDieRestart> _dieRestartDict;
        private readonly Dictionary<string, IFinish> _finishDict;
        private readonly Dictionary<string, IReset> _resetDict;

        public Dictionary<string, IInit> InitDict => _initDict;
        public Dictionary<string, ISave> SaveDict => _saveDict;
        public Dictionary<string, ILoad> LoadDict => _loadDict;
        public Dictionary<string, IDieRestart> DieRestartDict => _dieRestartDict;
        public Dictionary<string, IFinish> FinishDict => _finishDict;
        public Dictionary<string, IReset> ResetDict => _resetDict;

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

        public void ResetLevel(LevelData levelData)
        {
            foreach (var key in _finishDict.Keys)
            {
                //_resetDict[key].Reset(levelData);
            }
        }
    }
}
