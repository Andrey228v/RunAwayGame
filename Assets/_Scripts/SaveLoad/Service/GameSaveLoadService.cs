using Assets.Scripts.SaveLoad;
using System;
using System.Collections.Generic;
using System.Text;
using Assets.Scripts.SaveLoad.Data;

namespace Assets._Scripts.SaveLoad.Service
{
    public class GameSaveLoadService : IDisposable
    {
        private EasySaveSystem _saveSystem;
        private GameSaveData _saveData;
        private LevelConfig _currentLevel;

        private Dictionary<Type, ISaveLoadService> _services = new Dictionary<Type, ISaveLoadService>();
        
        public GameSaveLoadService(EasySaveSystem saveSystem) 
        {
            _saveSystem = saveSystem;

            LevelSaveLoadService levelSaveLoadService = new LevelSaveLoadService();
            AchievmentsSaveLoadService achievmentsSaveLoadService = new AchievmentsSaveLoadService();
            ShopSaveLoadService shopSaveLoadService = new ShopSaveLoadService();

            _services.Add(levelSaveLoadService.GetType(), levelSaveLoadService);
            _services.Add(achievmentsSaveLoadService.GetType(), achievmentsSaveLoadService);
            _services.Add(shopSaveLoadService.GetType(), shopSaveLoadService);

            LoadOrCreateSave();
        }

        public void Dispose() // это надо делать только при выходе из игры, потому что он общий для всех.
        {
            foreach (var key in _services.Keys)
            {
                _services[key].Dispose();
            }

            SaveGame();
        }

        public void SaveAllSevices()
        {
            foreach (var key in _services.Keys) 
            {
                _services[key].Save();
            }

            SaveGame();
        }

        public void LoadAllServices() 
        {
            foreach (var key in _services.Keys)
            {
                _services[key].Load();
            }
        }

        public void SaveGame()
        {
            _saveSystem.Save(SaveUtilites.GAME_SAVE_KEY, _saveData);
        }

        public void SetLevel(LevelConfig levelConfig)
        {
            _currentLevel = levelConfig;
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
    }
}
