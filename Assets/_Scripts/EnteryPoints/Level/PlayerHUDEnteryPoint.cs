using Assets.Scripts.UI;
using System;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class PlayerHUDEnteryPoint : IStartable
    {
        private Func<GamePanelController> _gamePanelFactory;

        public PlayerHUDEnteryPoint(Func<GamePanelController> gamePanelFactory) 
        {
            _gamePanelFactory = gamePanelFactory;
        }

        public void Start()
        {
            _gamePanelFactory();
        }
    }
}
