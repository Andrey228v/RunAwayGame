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
        private readonly Dictionary<string, IInit> _initList; // каждый подэлемент должен сам инициализировать то, что будет. 
        private readonly Dictionary<string, ISave> _saveList;
        private readonly Dictionary<string, ILoad> _loadList;
        private readonly Dictionary<string, IDieRestart> _dieRestartList;
        private readonly Dictionary<string, IFinish> _finishList;
        private readonly Dictionary<string, IReset> _resetList;




        //public void SaveAllServices(LevelData levelData)
        //{
        //    if (levelData == null)
        //    {
        //        throw new ArgumentNullException(nameof(levelData), "levelData cannot be null");
        //    }

        //    foreach (ISave save in _saveList)
        //    {
        //        save.Save(levelData);
        //    }

        //    _gameSaveLoadService.SaveGame();
        //}

        //public void LoadAllServices(LevelData levelData)
        //{
        //    if (levelData == null)
        //    {
        //        throw new ArgumentNullException(nameof(levelData), "levelData cannot be null");
        //    }

        //    if (_levelsController.Config == null)
        //    {
        //        return;
        //    }

        //    foreach (ILoad load in _loadList)
        //    {
        //        load.Load(levelData);
        //    }
        //}

        //public void DieRestart(LevelData levelData)
        //{
        //    foreach (IDieRestart restart in _dieRestartList)
        //    {
        //        restart.DieRestart(levelData);
        //    }
        //}

        //public void FinishLevel(LevelData levelData)
        //{
        //    if (levelData == null)
        //    {
        //        throw new ArgumentNullException(nameof(levelData), "gameSaveData cannot be null");
        //    }

        //    foreach (IFinish finish in _finishList)
        //    {
        //        finish.Finish(levelData);
        //    }
        //}

        //public void ResetLevel(LevelData levelData, LevelConfig levelConfig)
        //{
        //    if (levelData == null)
        //    {
        //        throw new ArgumentNullException(nameof(levelData), "gameSaveData cannot be null");
        //    }

        //    foreach (IReset reset in _resetList)
        //    {
        //        reset.ResetAllObjects(levelConfig);
        //    }
        //}
    }
}
