using Assets._Scripts.GameControllers.Levels;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.SaveLoad.Service
{
    public class GameSaveLoadService : IDisposable
    {
        private EasySaveSystem _saveSystem;
        private GameSaveData _gameSaveData;
        private LevelConfig _levelConfig;

        public GameSaveData GameSaveData => _gameSaveData;

        public Dictionary<Type, ISaveLoadService> Services { get; }

        public GameSaveLoadService(EasySaveSystem saveSystem) 
        {
            _saveSystem = saveSystem;
            Services = new Dictionary<Type, ISaveLoadService>();

            LoadOrCreateSave();
            InitializeAllServices();
            LoadAllServices();
        }

        public void Dispose() // это надо делать только при выходе из игры, потому что он общий для всех.
        {
            foreach (var key in Services.Keys)
            {
                Services[key].Dispose();
            }

            SaveGame();
        }

        public void InitializeAllServices()
        {
            foreach (var key in Services.Keys)
            {
                Services[key].Initialize();
            }
        }

        public void SaveAllServices()
        {
            foreach (var key in Services.Keys)
            {
                Services[key].SaveAllServices(_gameSaveData, _levelConfig);
            }

            SaveGame();
        }

        public void LoadAllServices() 
        {
            foreach (var key in Services.Keys)
            {
                Services[key].LoadAllServices(_gameSaveData, _levelConfig);
            }
        }

        public void AddSerice(ISaveLoadService service)
        {
            Services[service.GetType()] = service;
        }

        public void SetLevelConfig(LevelConfig levelConfig)
        {
            _levelConfig = levelConfig;
            //LoadAllServices();
        }

        public void ResetAllProgress()
        {
            _saveSystem.ResetAllProgress();
            _gameSaveData = new GameSaveData();
        }

        public T GetService<T>() where T : ISaveLoadService
        {
            if (Services.TryGetValue(typeof(T), out var service))
            {
                return (T)service;
            }

            throw new KeyNotFoundException($"Service of type {typeof(T)} not registered");
        }

        private void SaveGame()
        {
            _gameSaveData.LastSaveTime = DateTime.Now;
            _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _gameSaveData);
        }

        private void LoadOrCreateSave()
        {
            if (_saveSystem.HasKey(SaveUtilites.GAME_SAVE_KEY))
            {
                _gameSaveData = _saveSystem.Load<GameSaveData>(SaveUtilites.GAME_SAVE_KEY);
            }
            else
            {
                _gameSaveData = new GameSaveData();
                _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _gameSaveData);
            }
        }
    }
}
