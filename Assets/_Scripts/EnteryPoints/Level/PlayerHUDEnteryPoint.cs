using Assets._Scripts.GameControllers.Wallets;
using Assets.Scripts.UI;
using System;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class PlayerHUDEnteryPoint : IStartable
    {
        private Func<GamePanelView> _gamePanelFactory;
        private WalletController _walletController;

        public PlayerHUDEnteryPoint(Func<GamePanelView> gamePanelFactory, WalletController walletController) 
        {
            _walletController = walletController;
            _gamePanelFactory = gamePanelFactory;
        }

        public void Start()
        {
            GamePanelView panel = _gamePanelFactory();
            _walletController.AddGamePanelView(panel);

        }
    }
}
