using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.SaveLoad.Service;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SaveLoad
{
    public class SaveLoadService : ISaveService
    {
        private IEnumerable<ISaveLoad> _saveLoads;
        private ISaveSystem _saveSystem;
        private GameSaveData _saveData;

        public SaveLoadService(IEnumerable<ISaveLoad> saveLoads, ISaveSystem saveSystem) 
        {
            _saveLoads = saveLoads;
            _saveSystem = saveSystem;

            LoadOrCreateSave();
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

        public void LoadAllLevel()
        {
            foreach (ISaveLoad load in _saveLoads)
            {
                //load.Load();
            }
        }

        public void SaveAllLevel()
        {
            foreach (ISaveLoad load in _saveLoads)
            {
                //load.Save();
            }
        }


        public void DeleteSave()
        {
            
        }

        public LevelData GetLevelData(string levelID)
        {
            if (_saveData.LevelsData.TryGetValue(levelID, out LevelData data))
            {
                return data;
            }

            // Возвращаем новые данные, если для уровня нет сохранения
            return new LevelData { LevelID = levelID };
        }

        public bool HasSaveData()
        {
            return false;
        }


        public void SaveLevelData(string levelID, LevelData data)
        {
            
        }
    }
}
