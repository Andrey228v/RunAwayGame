using Assets._Scripts.GameControllers;
using Assets._Scripts.SaveLoad.Service;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Points
{
    public class CheckPointsController : IDisposable, IRestart //ISaveLoadService //ISaveLoadService //ISaveLoad
    {
        private Transform _checkPointsParent;
        private List<CheckPoint> _gameCheckPointList;
        private CheckPoint _lastCheckPointActiveted;

        public event Action OnSave;

        public CheckPointsController(GamePoints points)
        {
            if (points != null)
                _checkPointsParent = points.CheckPoints;
            else
                throw new ArgumentNullException(nameof(points), "CheckPoint parent cannot be null");

            _gameCheckPointList = TransformToList(_checkPointsParent);
        }

        public void Dispose()
        {
            //Под вопросом...

            foreach (CheckPoint checkPoint in _gameCheckPointList)
            {
                checkPoint.Dispose();
                checkPoint.OnActivated -= CheckPointActivated;
            }
        }

        public void Initialize()
        {

        }

        //Из трансформа собираем CheckPoints
        public List<CheckPoint> TransformToList(Transform checkPointsParent) 
        {
            if(checkPointsParent == null)
                throw new ArgumentNullException(nameof(checkPointsParent), "checkPointsParent cannot be null");

            List<CheckPoint> CheckPoints = new List<CheckPoint>();

            for (int i = 0; i < checkPointsParent.childCount; i++)
            {
                CheckPoint checkpoint = checkPointsParent.GetChild(i).GetComponent<CheckPoint>();
                CheckPoints.Add(checkpoint);
                checkpoint.OnActivated += CheckPointActivated;
            }

            return CheckPoints;
        }

        public void CheckPointActivated(CheckPoint checkPoint) // Вопрос надо ли передавать checkPoint
        {
            _lastCheckPointActiveted = checkPoint;
            OnSave?.Invoke();
        }

        public void Restart()
        {
            foreach(var checkPoint in _gameCheckPointList)
            {
                checkPoint.Deactivate();
            }
        }

        //public void AddSerice(ISaveLoadService service)
        //{

        //}

        public void SaveAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            var levelData = gameSaveData.LevelsData[levelConfig.LevelName];

            if (_lastCheckPointActiveted != null)
            {
                levelData.LastCheckPointPosition = _lastCheckPointActiveted.transform.position;
            }

            for (int i = 0; i < _gameCheckPointList.Count; i++)
            {
                levelData.CheckPoints[i] = new CheckPointData { Id = _gameCheckPointList[i].Id, IsActivated = _gameCheckPointList[i].IsActivated };
            }
        }

        public void LoadAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            var levelData = gameSaveData.LevelsData[levelConfig.LevelName];

            var checkpointsCount = _gameCheckPointList.Count;

            if (levelData.CheckPoints == null)
            {
                List<CheckPointData> loadCheckPointsData = new List<CheckPointData>();

                for (int i = 0; i < _gameCheckPointList.Count; i++)
                {
                    loadCheckPointsData.Add(new CheckPointData { Id = _gameCheckPointList[i].Id, IsActivated = _gameCheckPointList[i].IsActivated });
                }

                levelData.CheckPoints = loadCheckPointsData;
                Debug.Log(checkpointsCount);
            }
            else
            {
                for (int i = 0; i < checkpointsCount; i++)
                {
                    CheckPoint checkPoint = _gameCheckPointList[i];
                    CheckPointData checkPointData = levelData.CheckPoints[i];
                    checkPoint.SetId(checkPointData.Id); // ПОД ВОПРОСМ...
                    checkPoint.SetState(checkPointData.IsActivated);
                }
            }
        }


        //public void Load(LevelData levelData, LevelConfig levelConfig)
        //{
        //    var checkpointsCount = _gameCheckPointList.Count;

        //    if (levelData.CheckPoints == null)
        //    {
        //        List<CheckPointData> loadCheckPointsData = new List<CheckPointData>();

        //        for (int i = 0; i < _gameCheckPointList.Count; i++)
        //        {
        //            loadCheckPointsData.Add(new CheckPointData { Id = _gameCheckPointList[i].Id, IsActivated = _gameCheckPointList[i].IsActivated });
        //        }

        //        levelData.CheckPoints = loadCheckPointsData;
        //        Debug.Log(checkpointsCount);
        //    }
        //    else
        //    {
        //        for (int i = 0; i < checkpointsCount; i++)
        //        {
        //            CheckPoint checkPoint = _gameCheckPointList[i];
        //            CheckPointData checkPointData = levelData.CheckPoints[i];
        //            checkPoint.SetId(checkPointData.Id); // ПОД ВОПРОСМ...
        //            checkPoint.SetState(checkPointData.IsActivated);
        //        }
        //    }
        //}

        //public void Save(LevelData levelData)
        //{
        //    Debug.Log("SAVE COIN CONTROLLER");

        //    if(_lastCheckPointActiveted != null)
        //    {
        //        levelData.LastCheckPointPosition = _lastCheckPointActiveted.transform.position;
        //    }

        //    for (int i = 0; i < _gameCheckPointList.Count; i++)
        //    {
        //        levelData.CheckPoints[i] = new CheckPointData { Id = _gameCheckPointList[i].Id, IsActivated = _gameCheckPointList[i].IsActivated };
        //    }
        //}

    }
}
