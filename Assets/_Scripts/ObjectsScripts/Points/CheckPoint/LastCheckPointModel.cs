using Assets._Scripts.SaveLoad.Data;
using System;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points.CheckPoint
{
    public class LastCheckPointModel
    {
        private LastCheckPointData _position;

        public event Action<LastCheckPointData> OnTransformChanged;

        public LastCheckPointData Position => _position;

        public void SetTransorm(LastCheckPointData position)
        {
            _position = position;

            OnTransformChanged?.Invoke(position);
        }
    }
}
