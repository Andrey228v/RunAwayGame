using Assets._Scripts.SaveLoad.Service;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.GameControllers.Levels
{
    public class LevelsController : IStartable, IDisposable, ISaveLoadService
    {
        private bool _isLevelWasStart;

        public Dictionary<string, Level> _levels = new ();

        public Dictionary<Type, ISaveLoadService> Services { get; }

        public LevelsController()
        {
            _isLevelWasStart = false;
            Services = new Dictionary<Type, ISaveLoadService>();
        }

        public void Start()
        {
            _isLevelWasStart = true;
        }

        public void Dispose()
        {

        }

        public void Initialize()
        {
            // под вопросом...
        }

        public void AddSerice(ISaveLoadService service)
        {
            Services.Add(service.GetType(), service);
        }

        public void SaveAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            if(gameSaveData.LevelsData.TryGetValue(levelConfig.LevelName, out LevelData levelData))
            {
                levelData.IsLevelWasStarted = _isLevelWasStart;
            }
            else
            {
                LevelData newLevelData = new LevelData { };
                gameSaveData.LevelsData.Add(levelConfig.LevelName, newLevelData);
            }

            foreach (var key in Services.Keys)
            {
                Services[key].SaveAllServices(gameSaveData, levelConfig);
            }
        }

        public void LoadAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            foreach (var key in Services.Keys)
            {
                Services[key].LoadAllServices(gameSaveData, levelConfig);
            }
        }

        public void LoadLevel()
        {

        }
    }
}
