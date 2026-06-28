using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.UI._1MenuWindow;
using Assets._Scripts.UI._1MenuWindow.Achievements;
using Assets._Scripts.Utilites.Loger;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class MenuEnteryPoint : IStartable, IDisposable
    {
        private Func<MenuTabsView> _menuFactory;
        private Func<AchievmentsCellsView> _achievmentsCellsFactory;
        private Func<AchievementView> _achievmentsViewFactory;
        private AchievmentsController _achievmentsController;
        private WalletController _walletController;
        private IGameLogger _gameLogger;

        public MenuEnteryPoint(
            Func<MenuTabsView> menuFactory,
            AchievmentsController achievmentsController,
            IGameLogger gameLogger,
            WalletController walletController,
            Func<AchievementView> achievmentsViewFactory,
            Func<AchievmentsCellsView> achievmentsCellsFactory
            ) 
        {
            _menuFactory = menuFactory;
            _achievmentsCellsFactory = achievmentsCellsFactory;
            _achievmentsController = achievmentsController;
            _achievmentsViewFactory = achievmentsViewFactory;
            _walletController = walletController;
            _gameLogger = gameLogger;
        }

        public void Start()
        {
            InitMenu();

            //_achievmentsController.UpdateView();
            //_walletController.UpdateView();

        }

        public void Dispose()
        {
            _gameLogger.Log("MenuEnteryPoint OnDestroy", "Warning");
            _achievmentsController.Dispose();
            //_walletController.Dispose();
        }

        public void InitMenu()
        {
            MenuTabsView menuTabsView = _menuFactory();
            InitAchievments(menuTabsView.AchievmentsParent);

            _walletController.AddMenuView(menuTabsView);

        }

        private void InitAchievments(Transform parent) 
        {
            AchievmentsCellsView achievments = _achievmentsCellsFactory();
            GameObject cellsParent = achievments.CellsParent;
            achievments.transform.SetParent(parent, false);
            _achievmentsController.SetCellView(achievments);

            _gameLogger.Log("AchievmentsCellsView Construct", "Info");

            for (int i = 0; i < cellsParent.transform.childCount; i++)
            {
                _achievmentsController.AddCell(cellsParent.transform.GetChild(i));

                var achView = _achievmentsViewFactory();
                _achievmentsController.AddAchievmentView(achView, i);
            }
        }
    }
}
