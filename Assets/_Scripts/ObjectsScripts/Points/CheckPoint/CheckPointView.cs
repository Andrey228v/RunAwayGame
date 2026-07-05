using Assets._Scripts.ObjectsScripts.Player;
using System;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points.CheckPoint
{
    public class CheckPointView : MonoBehaviour
    {
        private string _id;

        public event Action<string, bool> OnActivateObject;

        public void Awake()
        {
            var t1 = gameObject.GetEntityId();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerMB>(out _) == false)
                return;

            OnActivateObject?.Invoke(_id, true);
        }

        private void OnDestroy()
        {
            OnActivateObject = null;
        }

        public void UpdateView(bool isActivated)
        {
            transform.gameObject.SetActive(isActivated == false);
        }

        public void SetId(string id)
        {
            _id = id;
        }
    }
}
