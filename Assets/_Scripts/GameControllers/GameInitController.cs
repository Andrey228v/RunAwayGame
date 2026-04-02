using System;
using System.Collections.Generic;

namespace Assets._Scripts.GameControllers
{
    public class GameInitController : IDisposable
    {
        private HashSet<IInit> _subscribers;
        public event Action OnNotify;

        public GameInitController()
        {
            _subscribers = new HashSet<IInit>();
        }

        public void Dispose()
        {
            if (_subscribers != null)
            {
                _subscribers.Clear();
                _subscribers = null;
            }
        }

        public void NotifySubs()
        {
            OnNotify?.Invoke();

            foreach (var sub in _subscribers)
            {
                sub.StartInit();
            }
        }

        public void AddSub(IInit sub)
        {
            _subscribers.Add(sub);
        }

        public void AddSub(IEnumerable<IInit> subList)
        {
            foreach (var sub in subList)
            {
                AddSub(sub);
            }
        }
    }
}
