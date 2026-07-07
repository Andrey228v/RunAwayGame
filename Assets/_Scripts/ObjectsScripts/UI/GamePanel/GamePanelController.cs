using Assets._Scripts.SaveLoad.Data.Interfaces;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.UI;
using System;


namespace Assets._Scripts.ObjectsScripts.UI.GamePanel
{
    public class GamePanelController : IDisposable, ISave, ILoad, IDieRestart, IFinish
    {
        private GamePanelView _gamePanelView;
        private GamePanelModel _gamePanelModel;

        public GamePanelController(GamePanelModel gamePanelModel)
        {
            _gamePanelModel = gamePanelModel;

            _gamePanelModel.OnWindowChange += WindowViewTrensit;
        }

        public void Dispose()
        {
            _gamePanelView.GameInterfacePanelView.OnTransitFromGameToMenu -= WindowTransit;
            _gamePanelView.GameWinPanelView.OnButtonBackToGameClick -= WindowTransit;
            _gamePanelView.GameMenuPanelView.OnButtonBackToGameClick -= WindowTransit;
        }

        public void AddGamePaneView(GamePanelView gamePanelView)
        {
            _gamePanelView = gamePanelView;

            _gamePanelView.GameInterfacePanelView.OnTransitFromGameToMenu += WindowTransit;
            _gamePanelView.GameWinPanelView.OnButtonBackToGameClick += WindowTransit;
            _gamePanelView.GameMenuPanelView.OnButtonBackToGameClick += WindowTransit;
        }

        public void WindowTransit(WindowType type)
        {
            _gamePanelModel.SetWindow(type);
        }

        public void WindowViewTrensit(WindowType type)
        {
            _gamePanelView.ShowPanel(type);
        }

        public void Save(LevelData levelData)
        {

        }

        public void Load(LevelData levelData)
        {

        }

        public void DieRestart(LevelData levelData)
        {

        }

        public void Finish(LevelData levelData)
        {
            _gamePanelModel.SetWindow(WindowType.WinPanel);
        }
    }
}
