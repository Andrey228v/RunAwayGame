using System;
using System.Collections.Generic;

namespace Assets._Scripts.ObjectsScripts.Points.CheckPoint
{
    public class CheckPointDictinaryModel
    {
        private readonly Dictionary<string, CheckPointModel> _objectModels;

        public event Action<CheckPointModel> OnCoinAdd;

        public Dictionary<string, CheckPointModel> ObjectModelds => _objectModels;

        public CheckPointDictinaryModel()
        {
            _objectModels = new Dictionary<string, CheckPointModel>();
        }

        public void Dispose()
        {
            foreach (var model in _objectModels.Values)
            {

            }
        }

        public void AddCoin(string id)
        {
            CheckPointModel model = new CheckPointModel(id);
            _objectModels.Add(id, model);
            OnCoinAdd?.Invoke(model);
        }
    }
}
