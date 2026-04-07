using Assets.Scripts.Points;
using System;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points
{
	public class RoadPoint: MonoBehaviour
	{

        public event Action<RoadPoint> OnActivated;

        private void OnTriggerEnter(Collider other)
        {

        }

    }
}