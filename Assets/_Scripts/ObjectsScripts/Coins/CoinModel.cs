using System;

namespace Assets._Scripts.ObjectsScripts.Coins
{
    public class CoinModel
    {
        private bool _isActivate;
        private readonly string _id;

        public event Action<string, bool> OnObjectStatusChange;
        public event Action<int> OnTake;

        public bool IsActivate => _isActivate;

        public CoinModel(string id)
        {
            _id = id;
            _isActivate = false;
        }

        public void SetActivateStatus(bool isActivate)
        {
            if (_isActivate == isActivate) return;

            _isActivate = isActivate;

            OnTake?.Invoke(1);
            OnObjectStatusChange?.Invoke(_id, _isActivate);
        }

        public void Reset()
        {
            SetActivateStatus(false);
        }
    }
}
