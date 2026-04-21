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
        public event Action<LevelConfig> OnFinishGame;
        public event Action OnRestartGame;

        public event Action OnLevelStart0;
        public event Action OnLevelStart1;
        public event Action OnLevelStart2;
        public event Action OnLevelFinish0;
        public event Action OnLevelFinish1;
        public event Action OnLevelFinish2;

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
            OnFinishGame?.Invoke(_levelConfig);
            _saveLoadService.FinishLevel(_levelConfig);
        }

        public void RestartGameSignal()
        {
            _saveLoadService.RestartLevel(_levelConfig);
            OnRestartGame?.Invoke();

        }

        public void StartLevel0()
        {
            OnLevelStart0?.Invoke();
        }

        public void StartLevel1()
        {
            OnLevelStart1?.Invoke();
        }

        public void StartLevel2()
        {
            OnLevelStart2?.Invoke();
        }

        public void FinishLevel0()
        {
            OnLevelFinish0?.Invoke();
        }

        public void FinishLevel1()
        {
            OnLevelFinish1?.Invoke();
        }

        public void FinishLevel2()
        {
            OnLevelFinish2?.Invoke();
        }
    }
}
