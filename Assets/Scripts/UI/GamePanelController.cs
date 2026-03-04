using Assets.Scripts.SaveLoad;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class GamePanelController : MonoBehaviour
    {
        [SerializeField] private GameInterfacePanel _gameInterfacePanel;
        [SerializeField] private GameMenuPanel _gameMenuPanel;

        private ISaveSystem _saveSystem;

        public GamePanelController(GameInterfacePanel gameInterfacePanel, GameMenuPanel gameMenuPanel,
            ISaveSystem saveSystem) 
        {
            _gameInterfacePanel = gameInterfacePanel;
            _gameMenuPanel = gameMenuPanel;
            _saveSystem = saveSystem;
        }

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
            //_saveSystem.Load();
        }

        private void SaveGame()
        {

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
