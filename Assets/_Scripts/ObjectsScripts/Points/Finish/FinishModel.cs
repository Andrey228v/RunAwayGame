using System;

namespace Assets._Scripts.ObjectsScripts.Points.Finish
{
    public class FinishModel
    {
        private bool _isActivate;

        public event Action OnFinishActivate;

        public FinishModel()
        {
            _isActivate = false;
        }

        public void SetActivateStatus(bool isActivate)
        {
            _isActivate = isActivate;
            OnFinishActivate?.Invoke();
        }

        public void Reset()
        {
            _isActivate = false;
        }
    }
}
