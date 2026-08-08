using System;

namespace Assets._Scripts.ObjectsScripts.Points.Finish
{
    public class FinishModel
    {
        private bool _isActivate;

        public event Action<bool> OnObjectStatusChange;
        public event Action OnFinish;

        public FinishModel()
        {
            _isActivate = false;
        }

        public void SetActivateStatus(bool isActivate)
        {
            _isActivate = isActivate;
            OnObjectStatusChange?.Invoke(_isActivate);
            OnFinish?.Invoke();
        }

        public void Reset()
        {
            _isActivate = true;
            OnObjectStatusChange?.Invoke(_isActivate);
        }
    }
}
