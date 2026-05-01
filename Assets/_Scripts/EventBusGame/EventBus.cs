using System;
using System.Collections.Generic;
using System.Text;

namespace Assets._Scripts.EventBusGame
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent eventToPublish);
    }

    public interface IEventSubscriber
    {
        void Subscribe<TEvent>(Action<TEvent> subscriberAction);
        void Unsubscribe<TEvent>(Action<TEvent> subscriberAction);
    }

    public class EventBus : IEventPublisher, IEventSubscriber
    {
        private readonly Dictionary<Type, List<object>> _subscriptions = new();

        public void Publish<TEvent>(TEvent eventToPublish)
        {
            var eventType = typeof(TEvent);
            if (!_subscriptions.ContainsKey(eventType)) return;

            // Создаём копию списка, чтобы избежать ошибок при изменении коллекции во время итерации
            var handlers = new List<object>(_subscriptions[eventType]);
            foreach (var handler in handlers)
            {
                var typedHandler = handler as Action<TEvent>;
                typedHandler?.Invoke(eventToPublish);
            }
        }

        public void Subscribe<TEvent>(Action<TEvent> subscriberAction)
        {
            var eventType = typeof(TEvent);
            if (!_subscriptions.ContainsKey(eventType))
            {
                _subscriptions[eventType] = new List<object>();
            }
            _subscriptions[eventType].Add(subscriberAction);
        }

        public void Unsubscribe<TEvent>(Action<TEvent> subscriberAction)
        {
            var eventType = typeof(TEvent);
            if (_subscriptions.ContainsKey(eventType))
            {
                _subscriptions[eventType].Remove(subscriberAction);
            }
        }
    }
}
