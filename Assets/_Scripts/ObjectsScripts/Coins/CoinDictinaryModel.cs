using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.ObjectsScripts.Coins
{
    public class CoinDictinaryModel
    {
        private readonly Dictionary<string, CoinModel> _objectModels;

        public event Action<CoinModel> OnCoinAdd;

        public Dictionary<string, CoinModel> ObjectModelds => _objectModels;

        public CoinDictinaryModel()
        {
            _objectModels = new Dictionary<string, CoinModel>();
        }

        public void Dispose()
        {
            foreach (var model in _objectModels.Values)
            {
                
            }
        }

        public void AddCoin(string id)
        {
            CoinModel model = new CoinModel(id);
            _objectModels.Add(id, model);
            OnCoinAdd?.Invoke(model);
        }


    }
}
