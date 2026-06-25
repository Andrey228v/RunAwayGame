using Assets._Scripts.EventBusGame;
using Assets._Scripts.GameControllers;
using ECM2;
using System;
using UnityEngine;
using VContainer;

namespace Assets._Scripts.ObjectsScripts.Player
{
    public class PlayerMB : MonoBehaviour, IDie
    {
        public event Action OnDie;

        //private IEventPublisher _eventBus;

        //[Inject]
        //public void Construct(IEventPublisher eventBus)
        //{
        //    _eventBus = eventBus;
        //}

        public void Die()
        {
            OnDie?.Invoke();

            //_eventBus.Publish(new DieEvent { Progress = 1 });
            //_eventBus.Publish(new CollectGoldEvent { Progress = count });
        }
    }
}
