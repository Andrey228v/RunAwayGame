using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.Utilites.Loger;
using System;
using System.Collections.Generic;


namespace Assets._Scripts.GameMVP.Achievments
{
    public class AchievmentDictinaryModel : IDisposable
    {
        private readonly Dictionary<string, AchievmentModel> _objectModels;

        private IGameLogger _gameLogger;

        public event Action<AchievmentModel> OnObjectAdd;

        public AchievmentDictinaryModel(IGameLogger gameLogger)
        {
            _objectModels = new Dictionary<string, AchievmentModel>();
            _gameLogger = gameLogger;
        }

        public void Dispose()
        {
            _objectModels.Clear();
        }

        public bool TryAddObject(string id, AchievmentData data, AchievmentsReward achievmentsReward)
        {
            bool isAdd = false;

            if (_objectModels.ContainsKey(id) == false)
            {
                AchievmentModel model = new AchievmentModel(data, achievmentsReward, _gameLogger);

                if (_objectModels.TryAdd(id, model) == false)
                    throw new ArgumentNullException("ERROR KEY");

                OnObjectAdd?.Invoke(model);

                isAdd = true;
            }

            return isAdd;
        }

        public bool TryGetModel(string id, out AchievmentModel model)
        {
            bool isFind = false;
            model = null;

            if (_objectModels.TryGetValue(id, out AchievmentModel value))
            {
                model = value;
                isFind = true;
            }

            return isFind;
        }

        public void Reset()
        {
            foreach(var key in _objectModels.Keys)
            {
                _objectModels[key].Reset();
            }
        }
    }
}
