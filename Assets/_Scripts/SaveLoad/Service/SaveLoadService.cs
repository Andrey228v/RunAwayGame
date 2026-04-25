using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.SaveLoad
{
    //public class SaveLoadService : IDisposable
    //{
    //    private HashSet<ISaveLoad> _saveLoads;
    //    private EasySaveSystem _saveSystem;
    //    private GameSaveData _saveData;
    //    private LevelConfig _levelConfig;

    //    public LevelConfig LevelConfig => _levelConfig;

    //    public GameSaveData SaveData => _saveData;

    //    public SaveLoadService(EasySaveSystem saveSystem) 
    //    {
    //        _saveLoads = new HashSet<ISaveLoad>();
    //        _saveSystem = saveSystem;

    //        LoadOrCreateSave();
    //    }

    //    public void Dispose() // это надо делать только при выходе из игры, потому что он общий для всех.
    //    {
    //        if(_saveLoads != null)
    //        {
    //            _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _saveData);
    //            _saveLoads.Clear();
    //            _saveLoads = null;
    //        }
    //    }

    //    public void SaveGameData()
    //    {
    //        _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _saveData);
    //    }

    //    public void SaveLevelData(LevelConfig levelConfig) // Сохранялка уровня.... сделать потом асинхронным SaveAsync
    //    {
    //        LevelData levelData = GetLevelData(levelConfig);

    //        foreach (ISaveLoad obj in _saveLoads)
    //        {
    //            obj.Save(levelData);
    //        }

    //        SaveLevelIntoMainDictinary(levelData, levelConfig);
    //    }

    //    public void LoadLevel(LevelConfig levelConfig) // загрузка... сделать потом асинхронной.
    //    {
    //        LevelData levelData = GetLevelData(levelConfig);

    //        foreach (ISaveLoad obj in _saveLoads)
    //        {
    //            obj.Load(levelData, _levelConfig);
    //        }
    //    }

    //    // функция, которая грузит часть объектов. Под вопросм.....
    //    // смысл в том, что после инициализации части элементов нам не надо грузить именно все...
    //    public void LoadPartLevelObject(IEnumerable<ISaveLoad> saveLoads, LevelConfig levelConfig) 
    //    {
    //        LevelData levelData = GetLevelData(levelConfig);

    //        foreach (var data in saveLoads)
    //        {
    //            data.Load(levelData, levelConfig);
    //        }
    //    }

    //    public void SetLevelConfig(LevelConfig levelConfig) // метод для присвоения ID уровня, который мы выбрали.
    //    {
    //        _levelConfig = levelConfig;
    //    }

    //    public void AddSaveLoadSub(ISaveLoad saveLoad) // Добавляем объекты, с которыми мы можем взаимодействовать..
    //    {
    //        _saveLoads.Add(saveLoad);
    //    }

    //    public void AddSaveLoadSub(IEnumerable<ISaveLoad> saveLoads) // добавляется в список сохраняемые объекты.
    //    {
    //        foreach (ISaveLoad obj in saveLoads) 
    //        {
    //            _saveLoads.Add(obj);
    //        }

    //        Debug.Log($"SaveLoadCount: {_saveLoads.Count()}");
    //    }

    //    public void DeleteSave(LevelConfig levelConfig) // пока не работает как надо... 
    //    {
    //        _saveData.LevelsData[levelConfig.LevelName] = new LevelData();
    //    }

    //    public void ResetAllProgress() 
    //    {
    //        Debug.Log("RESET ALL SAVE");
    //        _saveSystem.ResetAllProgress();
    //        _saveData = new GameSaveData();
    //    }

    //    public LevelData GetLevelData(LevelConfig levelConfig)
    //    {
    //        string levelName = levelConfig.LevelName;

    //        if (_saveData.LevelsData.TryGetValue(levelName, out LevelData levelData) == false)
    //        {
    //            levelData = new LevelData { LevelName = levelName };
    //            SaveLevelIntoMainDictinary(levelData, levelConfig);
    //        }

    //        return _saveData.LevelsData[levelName]; // можно передать просто data ??
    //    }

    //    private void SaveLevelIntoMainDictinary(LevelData data, LevelConfig levelConfig) // это сохранение в общий файл вопрос так ли это делать и на до ли....
    //    {
    //        string levelName = levelConfig.LevelName;

    //        if (_saveData.LevelsData.ContainsKey(levelName))
    //        {
    //            _saveData.LevelsData[levelName] = data;
    //        }
    //        else
    //        {
    //            _saveData.LevelsData.Add(levelName, data);
    //        }

    //        _saveData.CurrentLevelId = levelName;
    //        _saveData.LastSaveTime = DateTime.Now;
    //        _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _saveData);
    //    }

    //    private void LoadOrCreateSave()
    //    {
    //        if (_saveSystem.HasKey(SaveUtilites.GAME_SAVE_KEY))
    //        {
    //            _saveData = _saveSystem.Load<GameSaveData>(SaveUtilites.GAME_SAVE_KEY);
    //        }
    //        else
    //        {
    //            _saveData = new GameSaveData();
    //            _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _saveData);
    //        }
    //    }

    //    public void ClearList()
    //    {
    //        if(_saveLoads != null)
    //        {
    //            _saveLoads.Clear();
    //        }
    //    }
        
    //    //Непонятно что это...
    //    public void RestartLevel(LevelConfig levelConfig)
    //    {
    //        var data = GetLevelData(levelConfig);
    //    }

    //    public void FinishLevel(LevelConfig levelConfig)
    //    {
    //        SaveLevelData(levelConfig);
    //    }

    //    public void DieLoad()
    //    {

    //    }
    //}
}
