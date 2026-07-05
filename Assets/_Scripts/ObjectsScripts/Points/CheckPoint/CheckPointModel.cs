using Assets.Scripts.SaveLoad.Data;
using System;

namespace Assets._Scripts.ObjectsScripts.Points.CheckPoint
{
    public class CheckPointModel
    {
        //private bool _isActivate;
        //private readonly string _id;
        private CheckPointData _data;

        public event Action<string, bool> OnObjectStatusChange;
        public event Action<int> OnTakeValue;
        public event Action OnTake;

        public bool IsActivate => _data.IsActivated;

        public CheckPointModel(CheckPointData data)
        {
            _data = data;
            //_id = data.Id;
            //_isActivate = data.IsActivated;
        }

        public void SetActivateStatus(bool isActivate)
        {
            if (_data.IsActivated == isActivate) return;

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
