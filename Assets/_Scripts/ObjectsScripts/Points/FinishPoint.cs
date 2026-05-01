using Assets._Scripts.EventBusGame;
using Assets.Scripts.Player;
using System;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.Points
{
    public class FinishPoint : MonoBehaviour
    {
        //При достижении финиша мы получаем кубок, буст и нас кидает в начало...
        [SerializeField] private bool _isActivated = false;
        [SerializeField] private int _lvlName; // Переделать.... тут надо передавать это значение, а не передвать

        private bool _isInitialized;

        private IEventPublisher _eventBus;

        public bool IsActivated => _isActivated;
        
        public event Action OnFinishActivated;
        public event Action OnRestartActivated;

        private void Awake()
        {
            Initialize();
        }

        [Inject]
        public void Construct(IEventPublisher eventBus)
        {
            _eventBus = eventBus;
        }

        private void Initialize()
        {
            if (_isInitialized) return;

            _isActivated = false;
            _isInitialized = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsActivated) return;

            if (!other.TryGetComponent<PlayerMB>(out _)) return;

            Activate();
        }

        public void Activate()
        {
            if (_isActivated) return;

            _eventBus.Publish(new LevelCompletedEvent { });
        }
    }
}
