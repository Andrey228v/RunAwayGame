using System;
using System.Collections.Generic;


namespace Assets._Scripts.GameControllers
{
    public class ObserverController<T> : IDisposable
    {
        private HashSet<T> _subscribers;
        private Action<T> _notifyAction;
        public event Action OnNotify;

        public ObserverController(Action<T> notifyAction)
        {
            _subscribers = new HashSet<T>();
            _notifyAction = notifyAction;
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
                _notifyAction?.Invoke(sub);
            }
        }

        public void AddSub(T sub)
        {
            _subscribers.Add(sub);
        }

        public void AddSub(IEnumerable<T> subList)
        {
            foreach (var sub in subList)
            {
                AddSub(sub);
            }
        }
    }
}
