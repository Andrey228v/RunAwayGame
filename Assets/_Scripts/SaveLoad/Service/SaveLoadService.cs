using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.SaveLoad.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.SaveLoad
{
    public class SaveLoadService
    {
        private HashSet<ISaveLoad> _saveLoads;
        private ISaveSystem _saveSystem;
        private GameSaveData _saveData;
        private string _levelId;
        private LevelConfig _levelConfig;

        public SaveLoadService(ISaveSystem saveSystem) 
        {
            _saveLoads = new HashSet<ISaveLoad>();
            _saveSystem = saveSystem;

            LoadOrCreateSave();
        }

        public void SaveLevelIntoGame(LevelData data)
        {
            if (_saveData.LevelsData.ContainsKey(_levelId))
            {
                _saveData.LevelsData[_levelId] = data;
            }
            else
            {
                _saveData.LevelsData.Add(_levelId, data);
            }

            _saveData.CurrentLevelId = _levelId;
            _saveData.LastSaveTime = DateTime.Now;
            _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _saveData);
        }

        public void SetLevelId(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
            _levelId = _levelConfig.name;
        }

        public void SetLevelObjects(IEnumerable<ISaveLoad> saveLoads)
        {
            foreach (ISaveLoad obj in saveLoads) 
            {
                _saveLoads.Add(obj);
            }

            Debug.Log($"SaveLoadCount: {_saveLoads.Count()}");
        }

        private void LoadOrCreateSave()
        {
            if (_saveSystem.HasKey(SaveUtilites.GAME_SAVE_KEY))
            {
                _saveData = _saveSystem.Load<GameSaveData>(SaveUtilites.GAME_SAVE_KEY);
            }
            else
            {
                _saveData = new GameSaveData();
            }
        }

        public void LoadLevel()
        {
            LevelData levelData = GetLevelData();

            foreach (ISaveLoad obj in _saveLoads)
            {
                obj.Load(levelData);
            }
        }

        public void DeleteSave()
        {
            _saveSystem.Delete(_levelId);
        }

        public LevelData GetLevelData()
        {
            if (_saveData.LevelsData.TryGetValue(_levelId, out LevelData data))
            {
                return data;
            }

            // Возвращаем новые данные, если для уровня нет сохранения
            return new LevelData { LevelID = _levelId };
        }

        public void SaveLevelData()
        {
            LevelData data = GetLevelData();

            foreach (ISaveLoad obj in _saveLoads)
            {
                obj.Save(data);
            }

            SaveLevelIntoGame(data);
        }

        public LevelConfig GetLevelConfig()
        {
            return _levelConfig;
        }

        public void ClearSaveLoadList()
        {
            _saveLoads = new HashSet<ISaveLoad>();
        }


        //public async Task SaveLevelDataAsync()
        //{
        //    LevelData data = GetLevelData();

        //    // Сохраняем все объекты параллельно
        //    var saveTasks = new List<Task>();
        //    foreach (ISaveLoad obj in _saveLoads)
        //    {
        //        // Предполагаем, что у ISaveLoad есть асинхронный метод SaveAsync
        //        saveTasks.Add(obj.SaveAsync(data));
        //    }

        //    // Ждем завершения всех сохранений
        //    await Task.WhenAll(saveTasks);

        //    // Сохраняем уровень в файл
        //    await SaveLevelIntoGameAsync(data);

        //    Debug.Log("Асинхронное сохранение завершено");
        //}
    }
}
