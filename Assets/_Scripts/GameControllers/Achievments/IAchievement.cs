using Assets._Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.GameControllers.Achievments
{
    public interface IAchievement
    {
        public event Action OnUnlock;
        public event Action OnChanged;

        public AchievmentData Data { get; }

        public void ClaimReward();

        public AchievmentData GetData();

        public void SetData(AchievmentData data);
    }
}
