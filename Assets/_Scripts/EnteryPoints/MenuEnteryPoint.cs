using Assets._Scripts.GameControllers;
using Assets._Scripts.UI._1MenuWindow;
using Assets._Scripts.UI._1MenuWindow.Achievements;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class MenuEnteryPoint : IStartable, IDisposable
    {
        private Func<MenuTabs> _menuFactory;
        private Func<AchievmentsCellsView> _achievmentsCellsFactory;
        private Func<AchievementView> _achievmentsViewFactory;

        private MenuTabs _menuTabs;
        private AchievmentsCellsView _achievments;

        //private List<AchievementView> _achievementViews;

        public MenuEnteryPoint(Func<MenuTabs> menuFactory,
            Func<AchievmentsCellsView> achievmentsCellsFactory,
            Func<AchievementView> achievmentsViewFactory) 
        {
            _menuFactory = menuFactory;
            _achievmentsCellsFactory = achievmentsCellsFactory;
            _achievmentsViewFactory = achievmentsViewFactory;

            
        }

        public void Start()
        {
            InitMenu();
            InitAchievments();
        }

        public void Dispose()
        {
            //_menuTabs.OnChooseLevel -= _gameSaveLoadService.SetLevelConfig;
            //_menuTabs.OnSaveDelet -= _gameSaveLoadService.ResetAllProgress;

            //_menu.OnChooseLevel -= _saveLoadService.SetLevelConfig;
            //_menu.OnSaveDelet -= _saveLoadService.ResetAllProgress;

        }

        public void InitMenu()
        {
            _menuTabs = _menuFactory();
            _achievments = _achievmentsCellsFactory();
            _achievments.transform.SetParent(_menuTabs.AchievmentsParent, false);
        }

        public void InitAchievments()
        {
            //foreach (AchievmentModel ach in _gameSaveData.AchievmentsModels)
            //{
            //    var achView = _achievmentsViewFactory();
            //    achView.Construct(ach);
            //    _achievments.AddAchievment(achView);

            //    _achievementViews.Add(achView);
            //}

        }
    }
}
