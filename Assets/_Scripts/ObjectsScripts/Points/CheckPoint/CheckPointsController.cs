using Assets._Scripts.EventBusGame;
using Assets._Scripts.SaveLoad.Data;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points.CheckPoint
{
    public class CheckPointsController : ISave, ILoad, IRestart, IFinish
    {
        private IGameLogger _gameLogger;
        private Transform _checkPointsParent;
        private List<CheckPointView> _gameCheckPointList;
        private List<CheckPointData> _mapCheckPointsData;
        private CheckPointView _lastCheckPointActiveted;

        //private EventBus _eventBus;

        public CheckPointsController(GamePoints points, EventBus eventBus)
        {
            if (points != null)
                _checkPointsParent = points.CheckPoints;
            else
                throw new ArgumentNullException(nameof(points), "CheckPoint parent cannot be null");

            _gameCheckPointList = TransformToList(_checkPointsParent);

            //_eventBus = eventBus;
            //_eventBus.Subscribe<CheckPoinActivatedEvent>(CheckPointActivated);


        }

        public void Dispose()
        {
            //_eventBus.Unsubscribe<CheckPoinActivatedEvent>(CheckPointActivated);
        }

        //Из трансформа собираем CheckPoints
        public List<CheckPointView> TransformToList(Transform checkPointsParent) 
        {
            if(checkPointsParent == null)
                throw new ArgumentNullException(nameof(checkPointsParent), "checkPointsParent cannot be null");

            List<CheckPointView> CheckPoints = new List<CheckPointView>();

            for (int i = 0; i < checkPointsParent.childCount; i++)
            {
                CheckPointView checkpoint = checkPointsParent.GetChild(i).GetComponent<CheckPointView>();
                CheckPoints.Add(checkpoint);
            }

            return CheckPoints;
        }

        //public void CheckPointActivated(CheckPoinActivatedEvent args)
        //{
        //    _lastCheckPointActiveted = args.checkPoint;
        //}

        public void Finish(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "levelData cannot be null");
            }

            Restart(levelData);
        }

        public void Restart(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "levelData cannot be null");
            }

            foreach (var checkPoint in _gameCheckPointList)
            {
                checkPoint.Deactivate();
            }
        }

        public void Save(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "levelData cannot be null");
            }

            if (_lastCheckPointActiveted != null)
            {
                levelData.LastCheckPointPosition = _lastCheckPointActiveted.transform.position;
            }

            for (int i = 0; i < _gameCheckPointList.Count; i++)
            {
                levelData.CheckPoints[i] = new CheckPointData { Id = _gameCheckPointList[i].Id, IsActivated = _gameCheckPointList[i].IsActivated };
            }
        }

        public void Load(LevelData levelData)
        {
            if (levelData == null)
            {
                throw new ArgumentNullException(nameof(levelData), "levelData cannot be null");
            }

            var checkpointsCount = _gameCheckPointList.Count;

            if (levelData.CheckPoints == null || levelData.CheckPoints.Count == 0)
            {
                _mapCheckPointsData = new List<CheckPointData>();

                for (int i = 0; i < _gameCheckPointList.Count; i++)
                {
                    _mapCheckPointsData.Add(new CheckPointData { Id = _gameCheckPointList[i].Id, IsActivated = _gameCheckPointList[i].IsActivated });
                }

                levelData.CheckPoints = _mapCheckPointsData;
            }
            else
            {
                for (int i = 0; i < checkpointsCount; i++)
                {
                    CheckPointView checkPoint = _gameCheckPointList[i];
                    CheckPointData checkPointData = levelData.CheckPoints[i];
                    //checkPoint.SetId(checkPointData.Id); // ПОД ВОПРОСМ...
                    checkPoint.SetState(checkPointData.IsActivated);
                }
            }
        }
    }
}
