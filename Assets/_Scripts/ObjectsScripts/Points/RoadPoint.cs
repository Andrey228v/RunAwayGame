using Assets._Scripts.Bots;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using System;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points
{
	public class RoadPoint: MonoBehaviour
	{

        public event Action OnActivated;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<BotMB>(out _))
                OnActivated?.Invoke();
        }

    }
}