using System;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points.CheckPoint
{
    public class LastCheckPointModel
    {
        private Vector3 _position;

        public event Action<Vector3> OnTransformChanged;

        public Vector3 Position => _position;

        public LastCheckPointModel()
        {
            _position = Vector3.zero;
        }

        public void SetTransorm(Vector3 position)
        {
            _position = position;

            OnTransformChanged?.Invoke(position);
        }
    }
}
