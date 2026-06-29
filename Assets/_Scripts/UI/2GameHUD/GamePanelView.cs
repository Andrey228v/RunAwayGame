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

        private EventBus _eventBus;

        private LoadManager _loadManager;
        private List<SceneGroupHandle> _scensGroups;

        private IPanel _currentPanel;
        private List<IPanel> _panelsList = new List<IPanel>();

        public event Action OnDestroyView;

        public GameInterfacePanelView GameInterfacePanelView => _gameInterfacePanel;
        public GameMenuPanelView GameMenuPanelView => _gameMenuPanel;
        public GameWinPanelView GameWinPanelView => _gameWinPanel;

        [Inject]
        public void Constructor(LoadManager loadManager, 
            List<SceneGroupHandle> scensGroups, 
            EventBus eventBus)
        {
            _eventBus = eventBus;
            _loadManager = loadManager;
            _scensGroups = scensGroups;

            _currentPanel = _gameInterfacePanel;
        }

        //private void Start()
        //{
        //    //Переделать...
        //    _gameInterfacePanel.Name = "GameInterfacePanel";
        //    _gameMenuPanel.Name = "GameMenuPanel";
        //    _gameWinPanel.Name = "GameWinPanel";

        //    _panelsList.Add(_gameInterfacePanel);
        //    _panelsList.Add(_gameMenuPanel);
        //    _panelsList.Add(_gameWinPanel);
        //    //


        //    ShowPanel(_gameInterfacePanel.Name);
        //}

        //private void OnEnable()
        //{
        //    _eventBus.Subscribe<FinishLevelEvent>(FinishGame);
        //    _eventBus.Subscribe<TransitToPanelEvent>(OnShowPanel);
        //    _eventBus.Subscribe<TransitToWindowEvent>(OnBackToMenu);
        //}

        //private void OnDisable()
        //{
        //    _eventBus.Unsubscribe<FinishLevelEvent>(FinishGame);
        //    _eventBus.Unsubscribe<TransitToPanelEvent>(OnShowPanel);
        //    _eventBus.Unsubscribe<TransitToWindowEvent>(OnBackToMenu);
        //}

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
                _gameInterfacePanel.Show();
            }
            else if(windowType == WindowType.MenuPanel)
            {
                _gameMenuPanel.Show();
            }
            else if(windowType == WindowType.WinPanel)
            {
                _gameWinPanel.Show();
            }
        }

        private async void OnBackToMenu(TransitToWindowEvent args)
        {
            await _loadManager.LoadScene(_scensGroups[0]);
        }

        // под вопросом...
        //public void FinishGame(FinishLevelEvent args)
        //{
        //    ShowPanel("GameWinPanel");
        //}



        //Переделать...
        //private void ShowPanel(string panelName)
        //{



        //    //foreach (var panel in _panelsList) 
        //    //{
        //    //    if(panelName == panel.Name)
        //    //    {
        //    //        panel.Show();
        //    //    }
        //    //    else
        //    //    {
        //    //        panel.Hide();
        //    //    }
        //    //}
        //}

        //private void OnShowPanel(TransitToPanelEvent args)
        //{
        //    ShowPanel(args.windowName);
        //}


    }
}
