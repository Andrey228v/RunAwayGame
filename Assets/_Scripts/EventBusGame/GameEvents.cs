using Assets.Scripts.Points;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.EventBusGame
{
    public class GameEvents
    {

    }

    public struct LevelCompletedEvent { }

    public struct CheckPoinActivatedEvent 
    {
        public CheckPoint checkPoint;
    }

    public struct SaveGameEvent { }

    public struct LoadGameEvent { }

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

}
