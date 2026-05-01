using Assets._Scripts.EventBusGame;
using Assets.Scripts.Player;
using System;
using UnityEngine;
using VContainer;


namespace Assets.Scripts.Points
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private bool _isActivated;

        private bool _isInitialized;
        private IEventPublisher _eventBus;

        public string Id => _id;
        public bool IsActivated => _isActivated;

        [Inject]
        public void Construct(IEventPublisher eventBus)
        {
            _eventBus = eventBus;
        }

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (_isInitialized) 
                return;

            _isActivated = false;
            _isInitialized = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsActivated) 
                return;

            if (other.TryGetComponent<PlayerMB>(out _) == false) 
                return;

            Activate();
        }

        public void Activate()
        {
            if (_isActivated) 
                return;

            SetState(true);

            _eventBus.Publish(new CheckPoinActivatedEvent{checkPoint = this});
            _eventBus.Publish(new SaveGameEvent { });
        }

        public void Deactivate()
        {
            if (_isActivated == false) 
                return;

            SetState(false);
        }

        public void SetState(bool activated)
        {
            _isActivated = activated;
            gameObject.SetActive(!activated);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (string.IsNullOrEmpty(_id))
            {
                GenerateId();
            }
        }
#endif

        private void GenerateId()
        {
            _id = $"CP_{Guid.NewGuid():N}"[..12]; // Первые 12 символов
            Debug.Log($"[CheckPoint] Generated ID: {_id}", this);
        }

        // Публичные методы для управления (опционально)
        public void SetId(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                Debug.LogError("[CheckPoint] Cannot set empty ID!", this);
                return;
            }

            _id = id;
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
        }

        public void ResetState()
        {
            SetState(false);
        }
    }
}
