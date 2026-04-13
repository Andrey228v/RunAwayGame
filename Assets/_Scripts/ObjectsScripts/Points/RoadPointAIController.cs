using Assets.Scripts.Points;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points
{
	public class RoadPointAIController : IDisposable
	{
        private Transform _parent;
        private List<Transform> _gamePointList;
        private int _indexPoint = 0;
        private int _count;

        public event Action OnBotFinish;

        public void Dispose()
        {
            _gamePointList.Clear();
        }

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
            Vector3 position = Vector3.zero;

            if(_gamePointList.Count != 0 || _gamePointList != null)
            {
                if (_gamePointList[_indexPoint] != null) 
                {
                    position = _gamePointList[_indexPoint].transform.position;
                }
                else
                {
                    position = Vector3.zero;
                }
            }

            return position;
        }

        public Vector3 GetRandomPosition()
        {
            int indexRandom = UnityEngine.Random.Range(0, _count - 1);
            _indexPoint = indexRandom;

            return _gamePointList[indexRandom].transform.position;
        }

        public void SetRoadPointAIController(GamePoints points)
		{
            if (points != null)
                _parent = points.BotsRoad;
            else
                throw new ArgumentNullException(nameof(points), "CheckPoint parent cannot be null");

            _gamePointList = TransformToList(_parent);
            _count = _gamePointList.Count;
        }

        private List<Transform> TransformToList(Transform parent)
		{
            if (parent == null)
                throw new ArgumentNullException(nameof(parent), "parent cannot be null");


            List<Transform> Points = new List<Transform>();

            for (int i = 0; i < parent.childCount; i++)
            {
                Transform point = parent.GetChild(i).GetComponent<Transform>();
                Points.Add(point);
            }

            return Points;
        }

        public void Restart()
        {

            _indexPoint = 0;
        }
    }
}