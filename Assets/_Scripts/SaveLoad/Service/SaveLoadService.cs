using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.SaveLoad.Service;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SaveLoad
{
    public class SaveLoadService : ISaveService
    {
        private IEnumerable<ISaveLoad> _saveLoads;
        private ISaveSystem _saveSystem;
        private GameSaveData _saveData;
        private string _levelId;

        public SaveLoadService(ISaveSystem saveSystem) 
        {
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

        public void SetLevelId(string levelId)
        {
            _levelId = levelId;
        }

        public void SetLevelObjects(IEnumerable<ISaveLoad> saveLoads)
        {
            _saveLoads = saveLoads;
        }

        private void LoadOrCreateSave()
        {
            if (_saveSystem.HasKey(SaveUtilites.GAME_SAVE_KEY))
            {
                _saveData = _saveSystem.Load<GameSaveData>(SaveUtilites.GAME_SAVE_KEY);
                Debug.Log("Save loaded successfully");
            }
            else
            {
                _saveData = new GameSaveData();
                Debug.Log("New save created");
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
