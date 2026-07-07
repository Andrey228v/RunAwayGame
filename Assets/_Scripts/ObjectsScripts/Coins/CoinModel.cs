using Assets._Scripts.SaveLoad.Data;
using System;

namespace Assets._Scripts.ObjectsScripts.Coins
{
    public class CoinModel
    {
        private CoinData _data;

        public event Action<string, bool> OnObjectStatusChange;
        public event Action<int> OnTakeValue;
        public event Action OnTake;

        public bool IsActivate => _data.IsActivated;

        public CoinData Data => _data;

        public CoinModel(CoinData data)
        {
            _data = data;
        }

        public void SetActivateStatus(bool isActivate)
        {
            _data.IsActivated = isActivate;

            OnObjectStatusChange?.Invoke(_data.Id, _data.IsActivated);
        }

        public void Take()
        {
            OnTake?.Invoke();
            OnTakeValue?.Invoke(1);
        }

        public void Reset()
        {
            SetActivateStatus(false);
        }
    }
}
