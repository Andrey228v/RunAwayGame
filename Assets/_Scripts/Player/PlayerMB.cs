using Assets._Scripts.GameControllers;
using ECM2;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMB : MonoBehaviour, IDie
    {
        public event Action OnDie;

        public void Die()
        {
            OnDie?.Invoke();
        }
    }
}
