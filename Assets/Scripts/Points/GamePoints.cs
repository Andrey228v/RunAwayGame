using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Points
{
    public class GamePoints
    {
        public StartPoint StartPoint { get; private set; }
        public FinishPoint FinishPoint { get; private set; }

        public Transform CheckPoints { get; private set; }

        public GamePoints(StartPoint startPoint, FinishPoint finishPoint, Transform checkPoints)
        {
            StartPoint = startPoint;
            FinishPoint = finishPoint;
            CheckPoints = checkPoints;
        }
    }
}
