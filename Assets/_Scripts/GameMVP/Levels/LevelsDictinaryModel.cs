using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;


namespace Assets._Scripts.GameMVP.Levels
{
    public class LevelsDictinaryModel : IDisposable
    {
        private readonly Dictionary<string, LevelModel> _objectModels;
        private IGameLogger _gameLogger;

        public event Action<LevelModel> OnObjectAdd;

        public LevelsDictinaryModel(IGameLogger gameLogger)
        {
            _objectModels = new Dictionary<string, LevelModel>();
            _gameLogger = gameLogger;
        }

        public void Dispose()
        {
            _objectModels.Clear();
        }

        public bool TryAddObject(string id, LevelData data)
        {
            bool isAdd = false;

            if (_objectModels.ContainsKey(id) == false)
            {
                LevelModel model = new LevelModel(data);

                if (_objectModels.TryAdd(id, model) == false)
                    throw new ArgumentNullException("ERROR KEY");

                OnObjectAdd?.Invoke(model);

                isAdd = true;
            }

            return isAdd;
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
