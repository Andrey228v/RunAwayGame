using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Points
{
    public class CheckPointsController : ISaveLoad, IDisposable
    {
        public List<CheckPoint> CheckPoints { get; private set; }

        public event Action OnSave;

        public void Dispose()
        {
            foreach (CheckPoint checkPoint in CheckPoints)
            {
                checkPoint.OnActivated -= CheckPointActivated;
            }
        }

        public List<CheckPoint> TransformToList(Transform checkPoints) 
        {
            Transform checkPointsParent = checkPoints;
            CheckPoints = new List<CheckPoint>();

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
            OnSave?.Invoke();
        }

        public void Load(LevelData data)
        {
            for (int i = 0; i < CheckPoints.Count; i++)
            {
                CheckPoint checkPoint = CheckPoints[i];
                CheckPointData checkPointData = data.CheckPoints[i];
                checkPoint.SetId(checkPointData.Id); // ПОД ВОПРОСМ...
                checkPoint.SetState(checkPointData.IsActivated);
            }
        }

        public void Save(LevelData data)
        {
            for (int i = 0; i < CheckPoints.Count; i++)
            {
                data.CheckPoints[i] = new CheckPointData { Id = CheckPoints[i].Id, IsActivated = CheckPoints[i].IsActivated };
            }
        }
    }
}
