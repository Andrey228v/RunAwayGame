using Assets._Scripts.ObjectsScripts.Player;
using System;
using UnityEngine;
    
namespace Assets._Scripts.ObjectsScripts.Points.CheckPoint
{
    public class CheckPointView : MonoBehaviour
    {
        [SerializeField] private string _id;

        public event Action<string, bool, Vector3> OnActivateObject;

        public string Id => _id;

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (_id == null)
            {
                Debug.LogError($"{_id}: _id is not set!", this);
            }
        }
#endif

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<PlayerMB>(out _) == false)
                return;

            OnActivateObject?.Invoke(_id, true, gameObject.transform.position);
        }

        private void OnDestroy()
        {
            OnActivateObject = null;
        }

        public void UpdateView(bool isActivated)
        {
            transform.gameObject.SetActive(isActivated == false);
        }
    }
}
