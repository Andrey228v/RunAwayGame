using Assets._Scripts.GameControllers;
using Assets._Scripts.SaveLoad.Service;
using System;
using VContainer.Unity;

namespace Assets.Scripts.EnteryPoints
{
    public class GameEnteryPoint : IStartable, IDisposable
    {
        private GameFinishController _finishController;
        private GameRestartController _gameRestartController;
        private GameManager _gameManager;
        private GameSaveLoadService _gameSaveLoadService;

        public GameEnteryPoint(
            GameFinishController gameCycleController,
            GameRestartController gameRestartController,
            GameManager gameManager,
            GameSaveLoadService gameSaveLoadService)
        {
            _finishController = gameCycleController;
            _gameRestartController = gameRestartController;
            _gameManager = gameManager;
            _gameSaveLoadService = gameSaveLoadService;
        }

        public void Start()
        {
            _gameManager.OnSaveGame += SaveGame;
            _gameManager.OnLoadGame += LoadGame;
            _gameManager.OnFinishGame += FinishGame;
            _gameManager.OnRestartGame += RestartGame;
        }

        public void Dispose()
        {
            //_saveLoadService.ClearList(); // тут очищаем лист, под вопросом.
            _finishController.Dispose();
            _gameRestartController.Dispose();

            _gameManager.OnSaveGame -= SaveGame;
            _gameManager.OnLoadGame -= LoadGame;
            _gameManager.OnFinishGame -= FinishGame;
            _gameManager.OnRestartGame -= RestartGame;
        }

        public void SaveGame()
        {
            //_saveLoadService.SaveLevelData(_levelConfig);
            _gameSaveLoadService.SaveAllServices();
        }

        public void LoadGame()
        {
            //_saveLoadService.LoadLevel(_levelConfig);
            _gameSaveLoadService.LoadAllServices();
        }

        public void FinishGame()
        {
            _finishController.FinishNotifySubs();

            //if(levelConfig.SceneName == "")
        }

        public void RestartGame()
        {
            _gameRestartController.RestartNotifySubs();
        }
    }
}
