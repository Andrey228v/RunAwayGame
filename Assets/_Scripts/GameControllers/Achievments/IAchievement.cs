using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.GameControllers.Achievments
{
    public interface IAchievement
    {
        public event Action OnUnlock;
        public event Action OnChanged;

        string Id { get; }
        string Name { get; }
        string Description { get; }
        float Progress { get; }
        bool IsUnlocked { get; }
        bool IsClaimed { get; }
        public void ClaimReward();
    }
}
