using Assets.Scripts.Points;
using UnityEngine;

namespace Assets._Scripts.EventBusGame
{
    public class GameEvents
    {

    }

    public struct LevelCompletedEvent 
    {
        public string LvlId;
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
        public string AchievementId;
        public float Progress;
        public int CurrentValue;
        public int TargetValue;
    }

    public struct AchievementUnlockedEvent
    {
        public string AchievementId;
        public string Title;
        public string Description;
        public Sprite Icon;
    }

    public struct RewardClaimedEvent
    {
        public string AchievementId;
        public int RewardCoins;
    }

    public struct AddCoinsEvent
    {
        public int CoinCount;
    }

    public struct AddGobeletsEvent
    {
        public int GobeletCount;
    }

    public struct FinishLevelEvent
    {
        public LevelConfig levelConfig;
    }

    public struct StartLevel1 { }

    public struct StartLevel2 { }

    public struct StartLevel3 { }

    public struct FinishLevel1 { }
    public struct FinishLevel2 { }
    public struct FinishLevel3 { }

    public struct UpdateUIEvent { }

    public struct CollectGoldEvent { }

    public struct DieEvent { }
}
