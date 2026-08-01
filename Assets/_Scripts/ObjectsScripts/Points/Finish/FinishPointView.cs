using Assets._Scripts.ObjectsScripts.Player;
using System;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points.Finish
{
    public class FinishPointView : MonoBehaviour
    {
        private bool _isActivate = false;

        public event Action<bool> OnActivateObject;

        private void OnTriggerEnter(Collider other)
        {
            if(_isActivate == false)
            {
                if (other.TryGetComponent<PlayerMB>(out _) == false)
                    return;

                OnActivateObject?.Invoke(true);
            }
        }

        public void UpdateView(bool isActivate)
        {
            if (isActivate == true)
            {
                transform.gameObject.SetActive(false);
                _isActivate = isActivate;
            }
            else
            {
                transform.gameObject.SetActive(true);
            }
        }
    }
}
