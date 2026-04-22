using Assets.Scripts.SaveLoad;
using System.Collections.Generic;

namespace Assets._Scripts.SaveLoad.Service
{
    public class LevelSaveLoadService : ISaveLoadService
    {
        //private LevelConfig _levelConfig;

        //public void SetLevelConfig(LevelConfig levelConfig)
        //{
        //    _levelConfig = levelConfig;
        //}

        private HashSet<ISaveLoad> _saveLoads;

        public void AddSaveLoadSub(ISaveLoad saveLoad) // Добавляем объекты, с которыми мы можем взаимодействовать..
        {
            _saveLoads.Add(saveLoad);
        }

        public void Dispose()
        {

        }

        public void Save()
        {

        }

        public void Load() 
        {

        }


    }
}
