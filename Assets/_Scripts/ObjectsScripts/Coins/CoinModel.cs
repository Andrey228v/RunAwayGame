using System;

namespace Assets._Scripts.ObjectsScripts.Coins
{
    public class CoinModel
    {
        private bool _isActivate;
        private bool _isInitialize;

        public event Action<bool> OnObjectStatusChange;

        public CoinModel()
        {
            if (_isInitialize == true)
                return;

            _isActivate = false;
            _isInitialize = true;
        }

        public void SetActivateStatus(bool isActivate)
        {
            _isActivate = isActivate;
            OnObjectStatusChange?.Invoke(_isActivate);
        }

        public void Reset()
        {
            _isActivate = false;
            OnObjectStatusChange?.Invoke(_isActivate);
        }
    }
}
