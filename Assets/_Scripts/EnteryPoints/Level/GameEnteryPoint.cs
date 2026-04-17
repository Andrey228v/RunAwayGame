using Assets._Scripts.GameControllers;
using Assets.Scripts.SaveLoad;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.EnteryPoints
{
    public class GameEnteryPoint : IStartable, IDisposable
    {
        private SaveLoadService _saveLoadService;
        private GameFinishController _finishController;
        private GameRestartController _gameRestartController;
        private GameManager _gameManager;
        private LevelConfig _levelConfig;

        public GameEnteryPoint(SaveLoadService saveLoadService,
            GameFinishController gameCycleController,
            GameRestartController gameRestartController,
            GameManager gameManager)
        {
            _saveLoadService = saveLoadService;
            _finishController = gameCycleController;
            _gameRestartController = gameRestartController;
            _gameManager = gameManager;
            _levelConfig = _saveLoadService.LevelConfig;

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
            _saveLoadService.ClearList(); // тут очищаем лист, под вопросом.
            _finishController.Dispose();
            _gameRestartController.Dispose();

            _gameManager.OnSaveGame -= SaveGame;
            _gameManager.OnLoadGame -= LoadGame;
            _gameManager.OnFinishGame -= FinishGame;
            _gameManager.OnRestartGame -= RestartGame;
        }

        public void SaveGame()
        {
            _saveLoadService.SaveLevelData(_levelConfig);

        }

        public void LoadGame()
        {
            _saveLoadService.LoadLevel(_levelConfig);
        }

        public void FinishGame()
        {
            _finishController.FinishNotifySubs();
        }

        public void RestartGame()
        {
            _gameRestartController.RestartNotifySubs();
        }
    }
}
