using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.ObjectsScripts.Points.Finish;
using Assets._Scripts.ObjectsScripts.UI.GamePanel;
using Assets.Scripts.UI;
using System;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class PlayerHUDEnteryPoint : IStartable, IDisposable
    {
        private Func<GamePanelView> _gamePanelFactory;
        private WalletController _walletController;
        //private FinishModel _finishModel;
        private GamePanelController _gamePanelController;

        public PlayerHUDEnteryPoint(Func<GamePanelView> gamePanelFactory,
            //FinishModel finishModel,
            GamePanelController gamePanelController, 
            WalletController walletController) 
        {
            _walletController = walletController;
            _gamePanelFactory = gamePanelFactory;
            _gamePanelController = gamePanelController;
            //_finishModel = finishModel;
        }

        public void Start()
        {
            GamePanelView panel = _gamePanelFactory();
            _walletController.AddGamePanelView(panel);

            _gamePanelController.AddGamePaneView(panel);

            //_finishModel.OnFinishActivate += _gamePanelController.FinishActivated;

        }

        public void Dispose()
        {

        }
    }
}
