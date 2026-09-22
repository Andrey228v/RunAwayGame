using Assets._Scripts.GameControllers.Levels;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;


namespace Assets._Scripts.GameMVP.Levels
{
    public class LevelsDictinaryModel : IDisposable
    {
        private readonly Dictionary<string, LevelModel> _objectModels;

        public LevelsDictinaryModel()
        {
            _objectModels = new Dictionary<string, LevelModel>();
        }

        public void Dispose()
        {
            _objectModels.Clear();
        }

        public LevelModel AddObject(LevelData data)
        {
            LevelModel model = new LevelModel(data);

            if (_objectModels.TryAdd(data.Id, model) == false)
                throw new ArgumentNullException("ERROR KEY");

            return model;
        }

        public bool TryGetModel(string id, out LevelModel model)
        {
            bool isFind = false;
            model = null;

            if (_objectModels.TryGetValue(id, out LevelModel value))
            {
                model = value;
                isFind = true;
            }

            return isFind;
        }
    }
}
