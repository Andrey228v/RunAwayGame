using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Points
{
    public class CheckPointsController
    {
        public List<CheckPoint> CheckPoints { get; private set; }

        public List<CheckPoint> TransformToList(Transform checkPoints) 
        {
            Transform checkPointsParent = checkPoints;
            CheckPoints = new List<CheckPoint>();

            for (int i = 0; i < checkPointsParent.childCount; i++)
            {
                CheckPoint checkpoint = checkPointsParent.GetChild(i).GetComponent<CheckPoint>();
                CheckPoints.Add(checkpoint);
            }

            return CheckPoints;
        }


    }
}
