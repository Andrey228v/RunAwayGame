using System;
using UnityEngine;

namespace Assets.Scripts.SaveLoad
{
    [Serializable]
    public class PlayerData
    {
        public Vector3 PlayerPosition;
        public Quaternion PlayerRotation;

        public PlayerData()
        {
            PlayerPosition = Vector3.zero;
            PlayerRotation = Quaternion.identity;
        }
    }
}
