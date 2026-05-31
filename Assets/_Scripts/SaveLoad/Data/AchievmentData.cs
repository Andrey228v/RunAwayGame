using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.SaveLoad.Data
{
    [Serializable]
    public class AchievmentData
    {
        public string Id;
        public string Name;
        public string Description;
        public bool IsUnlock;
        public int TargetValue;
        public int CurrentValue;
        public bool IsClaimed;

        public float Progress => (float)CurrentValue / TargetValue;

        public bool CanClaim => IsUnlock && !IsClaimed;

        public void ResetData()
        {

        }
    }
}
