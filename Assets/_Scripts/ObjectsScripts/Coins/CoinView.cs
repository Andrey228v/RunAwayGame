using Assets._Scripts.ObjectsScripts.Player;
using System;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Coins
{
    public class CoinView : MonoBehaviour
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


        //public void Activate()
        //{
        //    if (_isActivated)
        //        return;

        //    SetState(true);
        //    OnActivated?.Invoke(this);
        //}

        //public void Deactivate()
        //{
        //    if (_isActivated == false)
        //        return;

        //    SetState(false);
        //}

        //public void SetState(bool activated)
        //{
        //    _isActivated = activated;
        //    gameObject.SetActive(!activated);
        //}



        //public void ResetState()
        //{
        //    SetState(false);
        //}
    }
}
