using System;

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
        public bool IsRevardEnable;

        public float Progress => (float)CurrentValue / TargetValue;

        public bool IsUnlockAndTaken => IsUnlock && !IsRevardEnable;

        public AchievmentData()
        {

        }

        public void ResetData()
        {

        }
    }
}
