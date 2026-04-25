using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.SaveLoad.Service
{
    public interface ISaveLoadService
    {
        public void Dispose();
        
        public void Initialize();

        public void AddSerice(ISaveLoadService service);

        public void SaveAllServices(GameSaveData gameSaveData, LevelConfig levelConfig);

        public void LoadAllServices(GameSaveData gameSaveData, LevelConfig levelConfig);
    }
}
