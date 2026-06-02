using Assets._Scripts.GameControllers.Wallets;
using Assets.Scripts.UI;
using System;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class PlayerHUDEnteryPoint : IStartable
    {
        private Func<GamePanelController> _gamePanelFactory;
        private WalletController _walletController;

        public PlayerHUDEnteryPoint(Func<GamePanelController> gamePanelFactory, WalletController walletController) 
        {
            _walletController = walletController;
            _gamePanelFactory = gamePanelFactory;
        }

        public void Start()
        {
            GamePanelController panel = _gamePanelFactory();
            _walletController.AddGamePanelView(panel);

        }
    }
}
