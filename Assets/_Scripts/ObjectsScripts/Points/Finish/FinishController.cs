using Assets._Scripts.SaveLoad.Data;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad.Data;
using System;

namespace Assets._Scripts.ObjectsScripts.Points.Finish
{
    public class FinishController : IFinish
    {
        private FinishPointView _finishPointView;
        private FinishModel _finishModel;

        public FinishController(GamePoints points, FinishModel finishModel)
        {
            if (points != null || finishModel != null)
            {
                _finishPointView = points.FinishPoint;
                _finishModel = finishModel;
            }
            else
                throw new ArgumentNullException(nameof(points), "CoinController parent cannot be null");

            _finishPointView.OnActivatePointView += SetActivateStatus;
            _finishModel.OnFinishActivate += UpdatePointView;
        }

        public void Dispose()
        {
            _finishPointView.OnActivatePointView -= SetActivateStatus;
            _finishModel.OnFinishActivate -= UpdatePointView;

            _finishPointView = null;
            _finishModel = null;
        }

        public void Finish(LevelData levelData)
        {
            _finishModel.Reset();
        }

        public void SetActivateStatus(bool status)
        {
            _finishModel.SetActivateStatus(status);
        }

        public void UpdatePointView()
        {
            _finishPointView.UpdateView(true);
        }
    }
}
