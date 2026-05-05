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
        }

        public void InitMenu()
        {
            _menuTabs = _menuFactory();
            _achievments = _achievmentsCellsFactory();
            _achievments.transform.SetParent(_menuTabs.AchievmentsParent, false);
        }

        public void InitAchievments()
        {
        }
    }
}
