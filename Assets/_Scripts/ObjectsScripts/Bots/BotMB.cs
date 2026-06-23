using Assets._Scripts.GameControllers;
using System;
using UnityEngine;

namespace Assets._Scripts.ObjectsScripts.Bots
{
    public class BotMB : MonoBehaviour, IDie
    {
        public event Action OnDie;

        public void Die()
        {
            OnDie?.Invoke();
        }
    }
}