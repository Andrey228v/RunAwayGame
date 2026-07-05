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

        public void AddObject(string id)
        {
            CoinModel model = new CoinModel(id);
            _objectModels.Add(id, model);
            OnObjectAdd?.Invoke(model);
        }
    }
}
