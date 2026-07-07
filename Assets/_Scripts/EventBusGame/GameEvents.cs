using Assets._Scripts.ObjectsScripts.Points.CheckPoint;
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
