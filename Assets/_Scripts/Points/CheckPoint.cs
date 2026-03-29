using Assets.Scripts.Player;
using System;
using UnityEngine;


namespace Assets.Scripts.Points
{
    public class CheckPoint : MonoBehaviour, IDisposable
    {
        [SerializeField] private string _id;
        [SerializeField] private bool _isActivated;

        private bool _isInitialized;

        public string Id => _id;
        public bool IsActivated => _isActivated;

        public event Action<CheckPoint> OnActivated;

        public void Dispose()
        {
            //надо ли тут что-то ???....
        }

        private void Awake()
        {
            Initialize();
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
            if (_isActivated) 
                return;

            SetState(true);
            OnActivated?.Invoke(this);
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
