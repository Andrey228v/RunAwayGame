using System;
using UnityEngine;

namespace Assets.Scripts.SaveLoad.Data
{
    [Serializable]
    public class CheckPointData
    {
        public string Id;
        public bool IsActivated;

        public void ResetData()
        {
            IsActivated = false;
        }
    }
}
