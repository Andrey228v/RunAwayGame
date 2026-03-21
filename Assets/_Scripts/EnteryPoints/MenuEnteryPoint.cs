using Assets._Scripts.SceneLoading;
using Assets._Scripts.UI._1MenuWindow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints
{
    public class MenuEnteryPoint : IStartable, IDisposable
    {
        private Func<MenuTabs> _menuFactory;
        private LoadScreenView _loadScreenView;

        public MenuEnteryPoint(LoadScreenView loadScreenView, Func<MenuTabs> menuFactory) 
        {
            Debug.Log(loadScreenView);
            _loadScreenView = loadScreenView;
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
