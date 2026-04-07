using Assets._Scripts.GameControllers;
using Assets.Scripts.Player;
using ECM2;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Scripts.Bots
{
    public class BotMB : MonoBehaviour, IDie
    {
        public event Action<BotMB> OnDie;

        public void Die()
        {
            OnDie?.Invoke(this);
        }
    }
}