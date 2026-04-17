using Assets.Scripts.SaveLoad;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers
{
    public class GameRestartController : IDisposable
    {
        private HashSet<IRestart> _restartListSubs;
        private SaveLoadService _saveLoadService;

        private LevelConfig _levelConfig;

        public event Action OnRestartLevel;

        public GameRestartController(SaveLoadService saveLoadService) 
        {
            _restartListSubs = new HashSet<IRestart>();
            _saveLoadService = saveLoadService;
            _levelConfig = saveLoadService.LevelConfig;
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
            _saveLoadService.DeleteSave(_levelConfig);

            OnRestartLevel?.Invoke();

            foreach (var sub in _restartListSubs)
            {
                sub.Restart();
            }

            _saveLoadService.LoadLevel(_levelConfig);
        }

        public void AddRestartSub(IRestart sub)
        {
            _restartListSubs.Add(sub);
        }

        public void AddRestartSub(IEnumerable<IRestart> subList)
        {
            foreach (var sub in subList)
            {
                AddRestartSub(sub);
            }
        }

        public void ClearRestartList()
        {
            _restartListSubs = new HashSet<IRestart>(); // Диспозим в местах где помещаем объекты.
        }
    }
}
