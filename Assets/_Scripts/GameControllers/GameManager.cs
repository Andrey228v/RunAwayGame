using Assets.Scripts.SaveLoad;
using System;

namespace Assets._Scripts.GameControllers
{
    //Класс который связывает всё эвентами
    public class GameManager : IDisposable
    {
        //private SaveLoadService _saveLoadService;

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

        //public GameManager(SaveLoadService saveLoadService)
        //{
        //    _saveLoadService = saveLoadService;
        //}

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
            //_levelConfig = _saveLoadService.LevelConfig; // переделать ... 
            //OnFinishGame?.Invoke(_levelConfig);
            //_saveLoadService.FinishLevel(_levelConfig);
        }

        public void RestartGameSignal()
        {
            //_levelConfig = _saveLoadService.LevelConfig; // переделать ... 
            //_saveLoadService.RestartLevel(_levelConfig);
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
