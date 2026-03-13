using Assets.Scripts.SaveLoad;
using System;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.UI
{
    public class GamePanelController : MonoBehaviour
    {
        [SerializeField] private GameInterfacePanel _gameInterfacePanel;
        [SerializeField] private GameMenuPanel _gameMenuPanel;

        public event Action OnButtonLoadClick;
        public event Action OnButtonSaveClick;

        private void OnEnable()
        {
            _gameInterfacePanel.OnMenuButtonClick += ShowMenu;
            _gameMenuPanel.OnBackButtonClick += HideMenu;

            _gameInterfacePanel.OnLoadButtonClick += LoadGame;
            _gameInterfacePanel.OnSaveButtonClick += SaveGame;
            _gameInterfacePanel.OnSoundButtonClick += SoundChangeState;
            _gameMenuPanel.OnBackButtonClick += BackToMenu;
        }

        private void OnDisable()
        {
            _gameInterfacePanel.OnMenuButtonClick -= ShowMenu;
            _gameMenuPanel.OnBackButtonClick -= HideMenu;

            _gameInterfacePanel.OnLoadButtonClick -= LoadGame;
            _gameInterfacePanel.OnSaveButtonClick -= SaveGame;
            _gameInterfacePanel.OnSoundButtonClick -= SoundChangeState;
            _gameMenuPanel.OnBackButtonClick -= BackToMenu;
        }

        private void ShowMenu()
        {
            //Pause
            //Music
            _gameInterfacePanel.Hide();
            _gameMenuPanel.Show();
        }

        private void HideMenu()
        {
            _gameInterfacePanel.Show();
            _gameMenuPanel.Hide();
        }

        private void LoadGame()
        {
            //_saveLoadService.LoadLevel();
            OnButtonLoadClick?.Invoke();
        }

        private void SaveGame()
        {
            //_saveLoadService.SaveLevelData();
            OnButtonSaveClick?.Invoke();
        }

        //?????
        private void SoundChangeState()
        {

        }

        //?????
        private void BackToMenu()
        {

        }
    }
}
