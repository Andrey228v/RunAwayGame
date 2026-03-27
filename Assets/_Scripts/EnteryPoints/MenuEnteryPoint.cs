using Assets._Scripts.UI._1MenuWindow;
using Assets.Scripts.SaveLoad;
using System;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class MenuEnteryPoint : IStartable, IDisposable
    {
        private Func<MenuTabs> _menuFactory;
        private SaveLoadService _saveLoadService;
        private MenuTabs _menu;

        public MenuEnteryPoint(Func<MenuTabs> menuFactory, SaveLoadService saveLoadService) 
        {
            _menuFactory = menuFactory;
            _saveLoadService = saveLoadService;
        }

        public void Start()
        {
            InitMenu();
        }

        public void Dispose()
        {
            _menu.OnChooseLevel -= SetLevelName;
            _menu.OnSaveDelet -= _saveLoadService.ResetAllProgress;
        }

        public void InitMenu()
        {
            _menu = _menuFactory();
            _menu.OnChooseLevel += SetLevelName;
            _menu.OnSaveDelet += _saveLoadService.ResetAllProgress;

        }

        private void SetLevelName(LevelConfig levelConfig)
        {
            _saveLoadService.SetLevelId(levelConfig);
        }
    }
}
