using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.Loger;
using Assets._Scripts.UI._1MenuWindow;
using Assets._Scripts.UI._1MenuWindow.Achievements;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class MenuEnteryPoint : IStartable, IDisposable
    {
        private Func<MenuTabsView> _menuFactory;
        private Func<AchievmentsCellsView> _achievmentsCellsFactory;
        private AchievmentsController _achievmentsController;
        private WalletController _walletController;
        private IGameLogger _gameLogger;

        public MenuEnteryPoint(
            Func<MenuTabsView> menuFactory,
            AchievmentsController achievmentsController,
            IGameLogger gameLogger,
            WalletController walletController,
            Func<AchievmentsCellsView> achievmentsCellsFactory
            ) 
        {
            _menuFactory = menuFactory;
            _achievmentsCellsFactory = achievmentsCellsFactory;
            _achievmentsController = achievmentsController;
            _walletController = walletController;
            _gameLogger = gameLogger;
        }

        public void Start()
        {
            InitMenu();

            _achievmentsController.UpdateView();
            _walletController.UpdateView();

        }

        public void Dispose()
        {
            _gameLogger.Log("MenuEnteryPoint OnDestroy", "Warning");
            _achievmentsController.Dispose();
        }

        public void InitMenu()
        {
            MenuTabsView menuTabs = _menuFactory();
            AchievmentsCellsView achievments = _achievmentsCellsFactory();
            achievments.transform.SetParent(menuTabs.AchievmentsParent, false);
            _achievmentsController.SetCellView(achievments);

            _walletController.AddMenuView(menuTabs);
        }
    }
}
