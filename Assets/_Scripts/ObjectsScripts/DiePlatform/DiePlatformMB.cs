using Assets._Scripts.EventBusGame;
using Assets._Scripts.GameControllers;
using UnityEngine;
using VContainer;

namespace Assets._Scripts.ObjectsScripts.DiePlatform
{
    public class DiePlatformMB : MonoBehaviour
    {


        private void OnTriggerEnter(Collider other)
        {

            if (other.TryGetComponent<IDie>(out IDie component))
            {
                component.Die();
            }
        }
    }
}
