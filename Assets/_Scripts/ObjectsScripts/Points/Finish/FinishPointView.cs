using Assets._Scripts.ObjectsScripts.Player;
using System;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Points.Finish
{
    public class FinishPointView : MonoBehaviour
    {
        public event Action<bool> OnActivateObject;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerMB>(out _) == false) 
                return;

            OnActivateObject?.Invoke(true);
        }

        public void UpdateView(bool isActivated)
        {
            if (isActivated == true)
            {
                transform.gameObject.SetActive(false);
            }
            else
            {
                transform.gameObject.SetActive(true);
            }
        }
    }
}
