using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.ObjectsScripts.Coins;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.Utilites.Loger;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.GameMVP.Achievments
{
    public class AchievmentDictinaryModel
    {
        private readonly Dictionary<string, AchievmentModel> _objectModels;
        private IGameLogger _gameLogger;

        public event Action<AchievmentModel> OnObjectAdd;

        public Dictionary<string, AchievmentModel> ObjectModelds => _objectModels;

        public AchievmentDictinaryModel(IGameLogger gameLogger)
        {
            _objectModels = new Dictionary<string, AchievmentModel>();
            _gameLogger = gameLogger;
        }

        public void AddObject(string id, AchievmentData data, AchievmentsReward achievmentsReward)
        {
            AchievmentModel model = new AchievmentModel(data, achievmentsReward, _gameLogger);

            if (_objectModels.TryAdd(id, model) == false)
                throw new ArgumentNullException("ERROR KEY");

            OnObjectAdd?.Invoke(model);
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
    }
}
