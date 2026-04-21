using Assets._Scripts.GameControllers;
using Assets._Scripts.GameControllers.Achievments;
using Assets._Scripts.UI._1MenuWindow;
using Assets._Scripts.UI._1MenuWindow.Achievements;
using Assets.Scripts.SaveLoad;
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

        private SaveLoadService _saveLoadService;
        private MenuTabs _menu;
        private AchievmentsCellsView _achievments;
        private GameManager _gameManger;

        private List<AchievmentModel> _achievmentsModels = new List<AchievmentModel>
        {
            new AchievmentModel("1", "fawfwa", false, false),
            new AchievmentModel("2", "fawfaf", false, false),
            new AchievmentModel("3", "123", true, false),
            new AchievmentModel("4", "123faw", true, true),
        };

        public MenuEnteryPoint(Func<MenuTabs> menuFactory, 
            SaveLoadService saveLoadService,
            Func<AchievmentsCellsView> achievmentsCellsFactory,
            Func<AchievementView> achievmentsViewFactory,
            GameManager gameManager) 
        {
            _menuFactory = menuFactory;
            _saveLoadService = saveLoadService;
            _achievmentsCellsFactory = achievmentsCellsFactory;
            _achievmentsViewFactory = achievmentsViewFactory;
            _gameManger = gameManager;
        }

        public void Start()
        {
            InitMenu();
        }

        public void Dispose()
        {
            _menu.OnChooseLevel -= _saveLoadService.SetLevelConfig;
            _menu.OnSaveDelet -= _saveLoadService.ResetAllProgress;
        }

        public void InitMenu()
        {
            _menu = _menuFactory();
            _achievments = _achievmentsCellsFactory();
            _achievments.transform.SetParent(_menu.AchievmentsParent, false);

            foreach(AchievmentModel ach in _achievmentsModels)
            {
                var achView = _achievmentsViewFactory();
                achView.Construct(ach);
                _achievments.AddAchievment(achView);
            }


            _menu.OnChooseLevel += _saveLoadService.SetLevelConfig;
            _menu.OnSaveDelet += _saveLoadService.ResetAllProgress;
        }
    }
}
