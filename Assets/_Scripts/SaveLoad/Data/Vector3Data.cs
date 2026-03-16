using System;
using UnityEngine;

namespace Assets.Scripts.SaveLoad.Data
{
    [Serializable]
    public struct Vector3Data
    {
        public float x, y, z;

        public Vector3Data(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}
