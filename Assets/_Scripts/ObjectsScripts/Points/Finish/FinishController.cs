using Assets._Scripts.SaveLoad.Data.Interfaces;
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

            _finishPointView.OnActivateObject += SetActivateStatus;
            _finishModel.OnObjectStatusChange += UpdatePointView;
        }

        public void Dispose()
        {
            _finishPointView.OnActivateObject -= SetActivateStatus;
            _finishModel.OnObjectStatusChange -= UpdatePointView;

            _finishPointView = null;
            _finishModel = null;
        }

        public void Finish(LevelData levelData)
        {
            _finishModel.Reset();

            //if (args.lvlId == "0")
            //{
            //    _eventBus.Publish(new FinishLevel1() { Progress = 1 });
            //}
            //else if (args.lvlId == "1")
            //{
            //    _eventBus.Publish(new FinishLevel2() { Progress = 1 });
            //}
            //else if (args.lvlId == "2")
            //{
            //    _eventBus.Publish(new FinishLevel3() { Progress = 1 });
            //}
        }

        public void SetActivateStatus(bool status)
        {
            _finishModel.SetActivateStatus(status);
        }

        public void UpdatePointView(bool isActivated)
        {
            _finishPointView.UpdateView(isActivated);
        }
    }
}
