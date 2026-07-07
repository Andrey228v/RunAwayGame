using Assets._Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.ObjectsScripts.Coins
{
    public class CoinDictinaryModel
    {
        private readonly Dictionary<string, CoinModel> _objectModels;

        public event Action<CoinModel> OnObjectAdd;

        public Dictionary<string, CoinModel> ObjectModelds => _objectModels;

        public CoinDictinaryModel()
        {
            _objectModels = new Dictionary<string, CoinModel>();
        }

        public void AddObject(CoinData data)
        {
            CoinModel model = new CoinModel(data);

            if(_objectModels.TryAdd(data.Id, model) == false)
                throw new ArgumentNullException("ERROR KEY");

            OnObjectAdd?.Invoke(model);
        }

        public bool TryGetModel(string id, out CoinModel model)
        {
            bool isFind = false;
            model = null;

            if (_objectModels.TryGetValue(id, out CoinModel value))
            {
                model = value;
                isFind = true;
            }

            return isFind;
        }
    }
}
