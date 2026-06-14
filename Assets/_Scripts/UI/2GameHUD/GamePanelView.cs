using Assets._Scripts.EventBusGame;
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
        private List<IPanel> _panelsList = new List<IPanel>();

        public event Action OnDestroyView;

        [Inject]
        public void Constructor(LoadManager loadManager, 
            List<SceneGroupHandle> scensGroups, 
            EventBus eventBus)
        {
            _eventBus = eventBus;
            _loadManager = loadManager;
            _scensGroups = scensGroups;
        }

        private void Start()
        {
            //Переделать...
            _gameInterfacePanel.Name = "GameInterfacePanel";
            _gameMenuPanel.Name = "GameMenuPanel";
            _gameWinPanel.Name = "GameWinPanel";

            _panelsList.Add(_gameInterfacePanel);
            _panelsList.Add(_gameMenuPanel);
            _panelsList.Add(_gameWinPanel);
            //


            ShowPanel(_gameInterfacePanel.Name);
        }

        private void OnEnable()
        {
            _eventBus.Subscribe<LevelCompletedEvent>(FinishGame);
            _eventBus.Subscribe<TransitToPanelEvent>(OnShowPanel);
            _eventBus.Subscribe<TransitToWindowEvent>(OnBackToMenu);
        }

        private void OnDisable()
        {
            _eventBus.Unsubscribe<LevelCompletedEvent>(FinishGame);
            _eventBus.Unsubscribe<TransitToPanelEvent>(OnShowPanel);
            _eventBus.Unsubscribe<TransitToWindowEvent>(OnBackToMenu);
        }

        private void OnDestroy()
        {
            OnDestroyView?.Invoke();
        }

        // под вопросом...
        public void FinishGame(LevelCompletedEvent args)
        {
            ShowPanel("GameWinPanel");
        }

        public void SetCoinsCountText(int actualCoin, int addCoin)
        {
            _gameInterfacePanel.SetCoinsCountText(actualCoin, addCoin);
        }

        //Переделать...
        private void ShowPanel(string panelName)
        {
            foreach (var panel in _panelsList) 
            {
                if(panelName == panel.Name)
                {
                    panel.Show();
                }
                else
                {
                    panel.Hide();
                }
            }
        }

        private void OnShowPanel(TransitToPanelEvent args)
        {
            ShowPanel(args.windowName);
        }

        private async void OnBackToMenu(TransitToWindowEvent args)
        {
            await _loadManager.LoadScene(_scensGroups[0]);
        }

        public void UpdateView()
        {

        }
    }
}
