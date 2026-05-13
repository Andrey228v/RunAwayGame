using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.SaveLoad.Data
{
    [Serializable]
    public class AchievmentsData
    {
        public string Id;
        public string Name;
        public string Description;
        public bool IsUnlock;
        public int TargetValue;
        public int CurrentValue;
        public bool IsClaimed;


        public void ResetData(LevelConfig levelConfig)
        {

        }
    }
}
