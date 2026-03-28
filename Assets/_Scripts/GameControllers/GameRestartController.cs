using Assets.Scripts.SaveLoad;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers
{
    public class GameRestartController : IDisposable
    {
        private HashSet<IRestart> _restartListSubs;
        private SaveLoadService _saveLoadService;

        public event Action OnRestartLevel;

        public GameRestartController(SaveLoadService saveLoadService) 
        {
            _restartListSubs = new HashSet<IRestart>();
            _saveLoadService = saveLoadService;
        }

        public void Dispose()
        {
            if (_restartListSubs != null) 
            {
                _restartListSubs.Clear();
                _restartListSubs = null;
            }
        }

        public void RestartNotifySubs()
        {
            _saveLoadService.DeleteSave();

            OnRestartLevel?.Invoke();

            foreach (var sub in _restartListSubs)
            {
                sub.Restart();
            }
        }


        public void ClearRestartList()
        {
            _restartListSubs = new HashSet<IRestart>(); // Диспозим в местах где помещаем объекты.
        }
    }
}
