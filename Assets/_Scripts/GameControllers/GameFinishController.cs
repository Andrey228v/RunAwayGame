using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers
{
    public class GameFinishController : IDisposable
    {
        private HashSet<IFinish> _finishListSubs;

        public event Action OnFinishLevel;

        public GameFinishController()
        {
            _finishListSubs = new HashSet<IFinish>();
        }

        public void Dispose()
        {
            if (_finishListSubs != null) 
            {
                _finishListSubs.Clear();
                _finishListSubs = null;
            }
        }

        public void FinishNotifySubs()
        {
            OnFinishLevel?.Invoke();

            foreach (var sub in _finishListSubs)
            {
                sub.FinishGame();
            }
        }

        public void AddFinishSub(IFinish sub)
        {
            _finishListSubs.Add(sub);
        }

        public void AddFinishSub(IEnumerable<IFinish> subList)
        {
            foreach (var sub in subList)
            {
                AddFinishSub(sub);
            }
        }
    }
}
