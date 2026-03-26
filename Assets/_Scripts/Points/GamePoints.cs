using UnityEngine;

namespace Assets.Scripts.Points
{
    public class GamePoints
    {
        public FinishPoint FinishPoint { get; private set; }

        public Transform CheckPoints { get; private set; }

        public GamePoints(FinishPoint finishPoint, Transform checkPoints)
        {
            FinishPoint = finishPoint;
            CheckPoints = checkPoints;
        }
    }
}
