using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.GameControllers.Levels;
using Assets._Scripts.GameControllers.Wallets;
using Assets._Scripts.SaveLoad.Service;
using Assets._Scripts.UI._1MenuWindow;
using Assets._Scripts.UI._1MenuWindow.Achievements;
using Assets._Scripts.Utilites.Loger;
using Assets.Scripts.SaveLoad.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class MenuEnteryPoint : IInitializable, IStartable, IDisposable
    {
        private GameSaveLoadService _gameSaveLoadService;
        private Func<MenuTabsView> _menuFactory;
        //private Func<AchievmentsCellsView> _achievmentsCellsFactory;
        private Func<AchievementView> _achievmentsViewFactory;
        private AchievmentsController _achievmentsController;
        private LevelsController _levelsController;
        private WalletController _walletController;
        private IGameLogger _gameLogger;

        public MenuEnteryPoint(
            Func<MenuTabsView> menuFactory,
            AchievmentsController achievmentsController,
            IGameLogger gameLogger,
            WalletController walletController,
            Func<AchievementView> achievmentsViewFactory,
            //Func<AchievmentsCellsView> achievmentsCellsFactory,
            GameSaveLoadService gameSaveLoadService,
            LevelsController levelsController
            ) 
        {
            _menuFactory = menuFactory;
            //_achievmentsCellsFactory = achievmentsCellsFactory;
            _achievmentsController = achievmentsController;
            _achievmentsViewFactory = achievmentsViewFactory;
            _walletController = walletController;
            _gameLogger = gameLogger;
            _levelsController = levelsController;
            _gameSaveLoadService = gameSaveLoadService;
        }

        public void Initialize()
        {
            MenuTabsView menuTabsView = _menuFactory();

            _levelsController.Initialization();
            _walletController.Initialization();
            _achievmentsController.Initialization(menuTabsView.AchievmentsParent);

            _walletController.AddMenuView(menuTabsView);
        }

        public void Start()
        {
            var gameSaveData = _gameSaveLoadService.GameSaveData;

            _levelsController.Load(gameSaveData);
            _walletController.Load(gameSaveData);
            _achievmentsController.Load(gameSaveData);
        }

        public void Dispose()
        {
            _gameLogger.Log("MenuEnteryPoint OnDestroy", "Warning");
            _achievmentsController.Dispose();
            //_walletController.Dispose();
        }
    }
}
