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
            for (int i = 0; i < CheckPoints.Count; i++)
            {
                OnSave -= CheckPointActivated;
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
                OnSave += CheckPointActivated;
            }

            return CheckPoints;
        }

        public void CheckPointActivated()
        {
            OnSave?.Invoke();
        }

        public void Load(LevelData data)
        {
            List<CheckPoint> loadCheckPointsData  = data.CheckPoints;

            for (int i = 0; i < CheckPoints.Count; i++)
            {
                CheckPoint checkPoint = CheckPoints[i];

                if (loadCheckPointsData[i].IsActivated == true)
                {
                    checkPoint.Activate();
                }
                else
                {
                    checkPoint.Deactivate();
                }
            }
        }

        public void Save(LevelData data)
        {
            data.CheckPoints = CheckPoints;
        }


    }
}
