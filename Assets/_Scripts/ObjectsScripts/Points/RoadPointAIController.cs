using Assets.Scripts.Points;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points
{
	public class RoadPointAIController
	{
        private Transform _parent;
        private List<RoadPoint> _gamePointList;
        private int _indexPoint = 0;

        public event Action OnBotFinish;
        
        public void AddPointCounter()
        {
            if(_indexPoint < _gamePointList.Count - 1)
            {
                _indexPoint++;
            }
            else
            {
                Restart();
                OnBotFinish?.Invoke();
            }
        }

        public Vector3 GetCurrentPoint()
        {
            return _gamePointList[_indexPoint].transform.position;
        }

        public void SetRoadPointAIController(GamePoints points)
		{
            if (points != null)
                _parent = points.BotsRoad;
            else
                throw new ArgumentNullException(nameof(points), "CheckPoint parent cannot be null");

            _gamePointList = TransformToList(_parent);
        }

        private List<RoadPoint> TransformToList(Transform parent)
		{
            if (parent == null)
                throw new ArgumentNullException(nameof(parent), "parent cannot be null");


            List<RoadPoint> Points = new List<RoadPoint>();

            for (int i = 0; i < parent.childCount; i++)
            {
                RoadPoint point = parent.GetChild(i).GetComponent<RoadPoint>();
                Points.Add(point);
                //point.OnActivated += AddPointCounter;
            }


            return Points;
        }

        public void Restart()
        {

            _indexPoint = 0;
        }

    }
}