using System;

namespace Assets._Scripts.GameControllers
{
    public class GameManager : IDisposable
    {
        public event Action OnSaveGame;
        public event Action OnLoadGame;
        public event Action OnFinishGame;
        public event Action OnRestartGame;

        public void Dispose()
        {
            
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
            //OnFinishGame?.Invoke();
        }

        public void RestartGameSignal()
        {
            OnRestartGame?.Invoke();
        }
    }
}
