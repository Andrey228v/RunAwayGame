using Assets.Scripts.Player;
using System;
using UnityEngine;

namespace Assets.Scripts.Points
{
    public class FinishPoint : MonoBehaviour
    {
        //При достижении финиша мы получаем кубок, буст и нас кидает в начало...
        [SerializeField] private bool _isActivated = false;

        public bool IsActivated => _isActivated;
        private bool _isInitialized;

        public event Action OnFinishActivated;
        public event Action OnRestartActivated;

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
            if (_isActivated) return;

            OnFinishActivated?.Invoke();
            OnRestartActivated?.Invoke();
        }

    }
}
