using Assets._Scripts.SaveLoad.Data;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad.Data;
using System;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points
{
    public class FinishController : ISave, ILoad, IRestart, IFinish
    {
        private FinishPoint _finishPoint;
        private FinishModel _finishModel;

        public FinishController(GamePoints points)
        {
            if (points != null)
                _finishPoint = points.FinishPoint;
            else
                throw new ArgumentNullException(nameof(points), "CoinController parent cannot be null");
        }

        public void Dispose()
        {
            _finishPoint = null;
        }

        public void Finish(LevelData levelData)
        {
            
        }

        public void Load(LevelData levelData)
        {
            
        }

        public void Restart(LevelData levelData)
        {
            
        }

        public void Save(LevelData levelData)
        {
            
        }
    }
}
