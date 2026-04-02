using Assets.Scripts.Player;
using System;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Coins
{
    public class Coin : MonoBehaviour, IDisposable
    {
        private bool _isActivated;
        private bool _isInitialized;

        public bool IsActivated => _isActivated;

        public event Action<Coin> OnActivated;

        public void Dispose()
        {

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

        private void OnTriggerEnter(Collider other)
        {
            if (IsActivated)
                return;

            if (other.TryGetComponent<PlayerMB>(out _) == false)
                return;

            Activate();
        }

        public void ResetState()
        {
            SetState(false);
        }
    }
}
