using Assets.Scripts.Points;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points
{
	public class RoadPointAIController
	{
        private Transform _parent;
        private List<RoadPoint> _gamePointList;

        public RoadPointAIController(GamePoints points)
		{
            if (points != null)
                _parent = points.CheckPoints;
            else
                throw new ArgumentNullException(nameof(points), "CheckPoint parent cannot be null");

            _gamePointList = TransformToList(_parent);
        }

        public List<RoadPoint> TransformToList(Transform parent)
		{
            if (parent == null)
                throw new ArgumentNullException(nameof(parent), "parent cannot be null");


            List<RoadPoint> Points = new List<RoadPoint>();

            for (int i = 0; i < parent.childCount; i++)
            {
                RoadPoint point = parent.GetChild(i).GetComponent<RoadPoint>();
                Points.Add(point);
                //point.OnActivated += CheckPointActivated;
            }


            return Points;
        }

    }
}