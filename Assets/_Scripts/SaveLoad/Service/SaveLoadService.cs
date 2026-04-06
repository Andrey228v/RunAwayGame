using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.SaveLoad
{
    public class SaveLoadService : IDisposable
    {
        private HashSet<ISaveLoad> _saveLoads;
        private EasySaveSystem _saveSystem;
        private GameSaveData _saveData;
        private string _levelId;
        private LevelConfig _levelConfig;

        public SaveLoadService(EasySaveSystem saveSystem) 
        {
            _saveLoads = new HashSet<ISaveLoad>();
            _saveSystem = saveSystem;

            LoadOrCreateSave();
        }

        public void Dispose() // это надо делать только при выходе из игры, потому что он общий для всех.
        {
            if(_saveLoads != null)
            {
                _saveLoads.Clear();
                _saveLoads = null;
            }
        }

        public void SaveLevelData() // Сохранялка уровня.... сделать потом асинхронным SaveAsync
        {
            LevelData levelData = GetLevelData();

            foreach (ISaveLoad obj in _saveLoads)
            {
                obj.Save(levelData);
            }

            SaveLevelIntoMainDictinary(levelData);
        }

        public void LoadLevel() // загрузка... сделать потом асинхронной.
        {
            LevelData levelData = GetLevelData();

            foreach (ISaveLoad obj in _saveLoads)
            {
                obj.Load(levelData, _levelConfig);
            }
        }

        // функция, которая грузит часть объектов. Под вопросм.....
        // смысл в том, что после инициализации части элементов нам не надо грузить именно все...
        public void LoadPartLevelObject(IEnumerable<ISaveLoad> saveLoads) 
        {
            LevelData levelData = GetLevelData();

            foreach (var data in saveLoads)
            {
                data.Load(levelData, _levelConfig);
            }
        }

        public void SetLevelId(LevelConfig levelConfig) // метод для присвоения ID уровня, который мы выбрали.
        {
            _levelConfig = levelConfig;
            _levelId = _levelConfig.name;
        }

        public void AddSaveLoadSub(ISaveLoad saveLoad) // Добавляем объекты, с которыми мы можем взаимодействовать..
        {
            _saveLoads.Add(saveLoad);
        }

        public void AddSaveLoadSub(IEnumerable<ISaveLoad> saveLoads) // добавляется в список сохраняемые объекты.
        {
            foreach (ISaveLoad obj in saveLoads) 
            {
                _saveLoads.Add(obj);
            }

            Debug.Log($"SaveLoadCount: {_saveLoads.Count()}");
        }

        public void DeleteSave() // пока не работает как надо... 
        {
            _saveData.LevelsData[_levelId] = new LevelData();
        }

        public void ResetAllProgress() 
        {
            Debug.Log("RESET ALL SAVE");
            _saveSystem.ResetAllProgress();
            _saveData = new GameSaveData();
        }

        public LevelData GetLevelData()
        {
            if (_saveData.LevelsData.TryGetValue(_levelId, out LevelData data) == false)
            {
                data = new LevelData { LevelID = _levelId };
                SaveLevelIntoMainDictinary(data);
            }


            return _saveData.LevelsData[_levelId]; // можно передать просто data ??
        }

        public LevelConfig GetLevelConfig()
        {
            return _levelConfig;
        }

        private void SaveLevelIntoMainDictinary(LevelData data) // это сохранение в общий файл вопрос так ли это делать и на до ли....
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

        private void LoadOrCreateSave()
        {
            if (_saveSystem.HasKey(SaveUtilites.GAME_SAVE_KEY))
            {
                _saveData = _saveSystem.Load<GameSaveData>(SaveUtilites.GAME_SAVE_KEY);
            }
            else
            {
                _saveData = new GameSaveData();
                _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _saveData);
            }
        }

        public void ClearList()
        {
            if(_saveLoads != null)
            {
                _saveLoads.Clear();
            }
        }
        
        public void RestartLevel()
        {
            var data = GetLevelData();
        }

        public void FinishLevel()
        {
            SaveLevelData();
        }

        public void DieLoad()
        {

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
