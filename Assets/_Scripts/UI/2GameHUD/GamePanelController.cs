using Assets._Scripts.GameControllers;
using Assets._Scripts.SceneLoading;
using Assets._Scripts.UI;
using Assets._Scripts.UI._2GameHUD;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.UI
{
    public class GamePanelController : MonoBehaviour, IFinish //Игровой HUD
    {
        [SerializeField] private GameInterfacePanel _gameInterfacePanel;
        [SerializeField] private GameMenuPanel _gameMenuPanel;
        [SerializeField] private GameWinPanel _gameWinPanel;

        public event Action OnButtonSaveClick;
        public event Action OnButtonLoadClick;
        public event Action OnRestartButtonClick;

        private LoadManager _loadManager;
        private List<SceneGroupHandle> _scensGroups;
        private List<IPanel> _panelsList = new List<IPanel>();

        [Inject]
        public void Constructor(LoadManager loadManager, List<SceneGroupHandle> scensGroups)
        {
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
            _gameInterfacePanel.OnMenuButtonClick += ShowPanel;
            _gameMenuPanel.OnBackToGameButtonClick += ShowPanel;

            _gameMenuPanel.OnBackToMenuButtonClick += BackToMenu;

 
            _gameInterfacePanel.OnLoadButtonClick += LoadGame;
            _gameInterfacePanel.OnSaveButtonClick += SaveGame;
            _gameInterfacePanel.OnSoundButtonClick += SoundChangeState;

            _gameWinPanel.OnReloudButtonClick += _gameWinPanel.Hide;
            _gameWinPanel.OnBackToMenuButtonClick += BackToMenu;
        }

        private void OnDisable()
        {
            _gameInterfacePanel.OnMenuButtonClick -= ShowPanel;
            _gameMenuPanel.OnBackToGameButtonClick -= ShowPanel;

            _gameMenuPanel.OnBackToMenuButtonClick -= BackToMenu;

            _gameInterfacePanel.OnLoadButtonClick -= LoadGame;
            _gameInterfacePanel.OnSaveButtonClick -= SaveGame;
            _gameInterfacePanel.OnSoundButtonClick -= SoundChangeState;

            _gameWinPanel.OnReloudButtonClick -= _gameWinPanel.Hide;
            _gameWinPanel.OnBackToMenuButtonClick -= BackToMenu;
        }

        // под вопросом...
        public void FinishGame()
        {
            ShowPanel("GameWinPanel");
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


        private void LoadGame()
        {
            OnButtonLoadClick?.Invoke();
        }

        private void SaveGame()
        {
            OnButtonSaveClick?.Invoke();
        }

        //?????
        private void SoundChangeState()
        {

        }

        private async void BackToMenu()
        {
            Debug.Log("EXIT to MENU");
            await _loadManager.LoadScene(_scensGroups[0]);
        }


    }
}
