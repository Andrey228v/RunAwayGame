using Assets.Scripts.SaveLoad;
using System;

namespace Assets._Scripts.GameControllers
{
    public class GameManager : IDisposable
    {
        private SaveLoadService _saveLoadService;

        public event Action OnInitGame;
        public event Action OnSaveGame;
        public event Action OnLoadGame;
        public event Action OnFinishGame;
        public event Action OnRestartGame;

        public GameManager(SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void Dispose()
        {
            
        }

        public void InitGameSignal()
        {
            OnInitGame?.Invoke();
        }

        public void SaveGameSignal()
        {
            OnSaveGame?.Invoke();
        }

        public void LoadGameSignal()
        {
            OnLoadGame?.Invoke();
        }

        public void FinishGameSignal()
        {
            OnFinishGame?.Invoke();
            _saveLoadService.FinishLevel();
        }

        public void RestartGameSignal()
        {
            _saveLoadService.RestartLevel();
            OnRestartGame?.Invoke();

        }
    }
}
