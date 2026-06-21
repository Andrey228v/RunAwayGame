using Assets.Scripts.Points;
using UnityEngine;

namespace Assets._Scripts.EventBusGame
{
    public class GameEvents
    {

    }

    public interface IAchivmentEvent
    {
        public int Progress { get; set; }
    }

    public struct LevelCompletedEvent 
    {
        public string lvlId;
    }

    public struct CheckPoinActivatedEvent 
    {
        public CheckPoint checkPoint;
    }

    public struct SaveGameEvent { }

    public struct LoadGameEvent { }

    public struct DeletSaveEvent { }

    public struct ButtonSoundChangeStateEvent { }

    public struct TransitToPanelEvent 
    {
        public string windowName;
    }

    public struct TransitToWindowEvent { }

    public struct ReloudButtonClickEvent { }

    public struct ChooseLevelEvent 
    {
        public LevelConfig levelConfig;
    }

    public struct AchievementProgressUpdatedEvent
    {
        public string achievementId;
        public float progress;
        public int currentValue;
        public int targetValue;
    }

    public struct AchievementUnlockedEvent
    {
        public string achievementId;
        public string title;
        public string description;
        public Sprite icon;
    }

    public struct RewardClaimedEvent
    {
        public string achievementId;
        public int rewardCoins;
    }

    public struct AddCoinsEvent
    {
        public int coinCount;
    }

    public struct AddGobeletsEvent
    {
        public int gobeletCount;
    }

    public struct FinishLevelEvent
    {
        public LevelConfig levelConfig;
    }

    public struct StartLevel1 : IAchivmentEvent
    {
        public int Progress { get; set; }
    }

    public struct StartLevel2 : IAchivmentEvent
    {
        public int Progress { get; set; }
    }

    public struct StartLevel3 : IAchivmentEvent
    {
        public int Progress { get; set; }
    }

    public struct FinishLevel1 : IAchivmentEvent
    {
        public int Progress { get; set; }
    }

    public struct FinishLevel2 : IAchivmentEvent
    {
        public int Progress { get; set; }
    }

    public struct FinishLevel3 : IAchivmentEvent
    {
        public int Progress { get; set; }
    }

    public struct UpdateUIEvent {}

    public struct CollectGoldEvent : IAchivmentEvent
    {
        public int Progress { get; set; }
    }

    public struct DieEvent : IAchivmentEvent
    {
        public int Progress { get; set; }
    }
}
