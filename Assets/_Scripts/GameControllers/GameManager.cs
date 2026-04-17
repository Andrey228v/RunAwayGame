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

        private LevelConfig _levelConfig;

        public GameManager(SaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _levelConfig = saveLoadService.LevelConfig;
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
            _saveLoadService.FinishLevel(_levelConfig);
        }

        public void RestartGameSignal()
        {
            _saveLoadService.RestartLevel(_levelConfig);
            OnRestartGame?.Invoke();

        }
    }
}
