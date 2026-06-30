using Assets._Scripts.EventBusGame;
using Assets._Scripts.ObjectsScripts.UI.GamePanel;
using Assets._Scripts.SceneLoading;
using Assets._Scripts.UI;
using Assets._Scripts.UI._2GameHUD;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.UI
{
    public class GamePanelView : MonoBehaviour //Игровой HUD
    {
        [SerializeField] private GameInterfacePanelView _gameInterfacePanel;
        [SerializeField] private GameMenuPanelView _gameMenuPanel;
        [SerializeField] private GameWinPanelView _gameWinPanel;

        private IPanel _currentPanel;

        public event Action OnDestroyView;

        public GameInterfacePanelView GameInterfacePanelView => _gameInterfacePanel;
        public GameMenuPanelView GameMenuPanelView => _gameMenuPanel;
        public GameWinPanelView GameWinPanelView => _gameWinPanel;

        private void Start()
        {
            _currentPanel = _gameInterfacePanel;
        }

        private void OnDestroy()
        {
            OnDestroyView?.Invoke();
        }

        public void SetCoinsCountText(int actualCoin, int addCoin)
        {
            _gameInterfacePanel.SetCoinsCountText(actualCoin, addCoin);
        }

        public void ShowPanel(WindowType windowType) // под вопросом...
        {
            _currentPanel.Hide();

            if (windowType == WindowType.InterfacePanel)
            {
                _currentPanel = _gameInterfacePanel;
                _gameInterfacePanel.Show();
            }
            else if(windowType == WindowType.MenuPanel)
            {
                _currentPanel = _gameMenuPanel;
                _gameMenuPanel.Show();
            }
            else if(windowType == WindowType.WinPanel)
            {
                _currentPanel = _gameWinPanel;
                _gameWinPanel.Show();
            }
        }
    }
}
