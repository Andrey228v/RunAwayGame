using Assets._Scripts.UI._1MenuWindow;
using System;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class MenuEnteryPoint : IStartable, IDisposable
    {
        private Func<MenuTabs> _menuFactory;

        public MenuEnteryPoint(Func<MenuTabs> menuFactory) 
        {
            _menuFactory = menuFactory;
        }

        public void Start()
        {
            InitMenu();
        }

        public void Dispose()
        {
           
        }

        public void InitMenu()
        {
            MenuTabs menu = _menuFactory();
        }
    }
}
