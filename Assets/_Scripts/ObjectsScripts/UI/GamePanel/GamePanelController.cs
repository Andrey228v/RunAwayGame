using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.ObjectsScripts.UI.GamePanel
{
    public class GamePanelController : IDisposable
    {
        private GamePanelView _gamePanelView;
        private GamePanelModel _gamePanelModel;

        public GamePanelController(GamePanelModel gamePanelModel)
        {
            _gamePanelModel = gamePanelModel;
        }

        public void Dispose()
        {
            _gamePanelView.GameInterfacePanelView.OnTransitFromGameToMenu -= WindowTransit;
        }

        public void AddGamePaneView(GamePanelView gamePanelView)
        {
            _gamePanelView = gamePanelView;

            _gamePanelView.GameInterfacePanelView.OnTransitFromGameToMenu += WindowTransit;
        }

        public void FinishActivated()
        {
            _gamePanelModel.SetWindow(WindowType.WinPanel);
        }

        public void WindowTransit(WindowType type)
        {
            _gamePanelModel.SetWindow(type);
        }

    }
}
