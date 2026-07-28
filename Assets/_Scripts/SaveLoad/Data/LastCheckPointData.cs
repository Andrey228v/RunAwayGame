using System;
using UnityEngine;

namespace Assets._Scripts.SaveLoad.Data
{
    [Serializable]
    public class LastCheckPointData
    {
        public Vector3 position;
        public Quaternion rotation;
    }
}
