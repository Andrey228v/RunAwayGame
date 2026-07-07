using System;

namespace Assets._Scripts.ObjectsScripts.Points.CheckPoint
{
    public class CheckPointModel
    {
        private bool _isActivate;
        private readonly string _id;

        public event Action<string, bool> OnObjectStatusChange;
        public event Action<int> OnTakeValue;
        public event Action OnTake;

        public bool IsActivate => _isActivate;

        public CheckPointModel(string id)
        {
            _id = id;
            _isActivate = false;
        }

        public void SetActivateStatus(bool isActivate)
        {
            if (_isActivate == isActivate) return;

            _isActivate = isActivate;

            OnObjectStatusChange?.Invoke(_id, _isActivate);
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
