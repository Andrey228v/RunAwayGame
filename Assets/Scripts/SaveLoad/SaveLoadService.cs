using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.SaveLoad
{
    public class SaveLoadService 
    {
        private IEnumerable<ISaveLoad> _saveLoads;

        public SaveLoadService(IEnumerable<ISaveLoad> saveLoads) 
        {
            _saveLoads = saveLoads;
            
        }

        public void Load()
        {
            foreach (ISaveLoad load in _saveLoads)
            {
                load.Load();
            }
        }

        public void Save()
        {
            foreach (ISaveLoad load in _saveLoads)
            {
                load.Save();
            }
        }
    }
}
