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
        private GameCycleController _cycleController;
        private GameRestartController _gameRestartController;
        private GameManager _gameManager;

        public GameEnteryPoint(SaveLoadService saveLoadService,
            GameCycleController gameCycleController,
            GameRestartController gameRestartController,
            GameManager gameManager)
        {
            _saveLoadService = saveLoadService;
            _cycleController = gameCycleController;
            _gameRestartController = gameRestartController;
            _gameManager = gameManager;
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
            _cycleController.Dispose();
            _gameRestartController.Dispose();

            _gameManager.OnSaveGame -= SaveGame;
            _gameManager.OnLoadGame -= LoadGame;
            _gameManager.OnFinishGame -= FinishGame;
            _gameManager.OnRestartGame -= RestartGame;
        }

        public void SaveGame()
        {
            Debug.Log("SAVE GAME");
            _saveLoadService.SaveLevelData();

        }

        public void LoadGame()
        {
            Debug.Log("LOAD GAME");
            _saveLoadService.LoadLevel();
        }

        public void FinishGame()
        {
            Debug.Log("FINISH GAME");
            _cycleController.FinishNotifySubs();
        }

        public void RestartGame()
        {
            Debug.Log("RESTART GAME");
            _gameRestartController.RestartNotifySubs();
        }
    }
}
