using Assets._Scripts.GameControllers;
using UnityEngine;

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
