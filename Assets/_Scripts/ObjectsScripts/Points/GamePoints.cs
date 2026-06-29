using Assets._Scripts.ObjectsScripts.Points.Finish;
using UnityEngine;

namespace Assets.Scripts.Points
{
    public class GamePoints
    {
        public FinishPointView FinishPoint { get; private set; }

        public Transform CheckPoints { get; private set; }

        public Transform Coins { get; private set; }

        public  Transform BotsRoad { get; private set; }

        public GamePoints(FinishPointView finishPoint, Transform checkPoints, Transform coins, Transform botsRoad)
        {
            FinishPoint = finishPoint;
            CheckPoints = checkPoints;
            Coins = coins;
            BotsRoad = botsRoad;
        }
    }
}
