using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers
{
    public class GameCycleController : IDisposable
    {
        private List<IFinish> _finishListSubs;
        private List<IRestart> _restartSubs;

        public event Action OnFinishGame;
        public event Action OnRestartGame;

        public GameCycleController(List<IFinish> finishListSubs, List<IRestart> restartSubs)
        {
            _finishListSubs = finishListSubs;
            _restartSubs = restartSubs;
        }

        public void Dispose()
        {
            _finishListSubs = null;
        }

        public void FinishNotifySubs()
        {
            foreach (var sub in _finishListSubs)
            {
                sub.FinishGame();
            }
        }

        public void AddFinishSub(IFinish sub)
        {
            _finishListSubs.Add(sub);
        }

        public void RestartNotifySubs()
        {

        }
    }
}
