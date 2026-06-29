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

    public struct FinishLevelEvent 
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

    //public struct UpdateUIEvent {}

    public struct CollectGoldEvent : IAchivmentEvent
    {
        public int Progress { get; set; }
    }

    public struct DieEvent : IAchivmentEvent
    {
        public int Progress { get; set; }
    }
}
