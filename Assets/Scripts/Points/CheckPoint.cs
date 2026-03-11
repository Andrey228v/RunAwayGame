using Assets.Scripts.Player;
using UnityEngine;


namespace Assets.Scripts.Points
{
    public class CheckPoint : MonoBehaviour
    {
        [SerializeField] private string _id;

        public string Id => _id;

        public bool IsActivated { get; private set; }

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Автоматически генерируем GUID в редакторе
            if (string.IsNullOrEmpty(_id))
            {
                GenerateGUID();
            }
        }
#endif

        private void Awake()
        {
            IsActivated = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (IsActivated)
            {
                return;
            }

            if (other.GetComponent<PlayerMB>() == false)
            {
                return;
            }

            Activate();
        }

        public void Activate()
        {
            IsActivated = true;
            gameObject.SetActive(false);
        }

        public void Deactivate()
        {
            IsActivated = false;
            gameObject.SetActive(true);
        }






        private void GenerateGUID()
        {
            _id = $"CP_{System.Guid.NewGuid().ToString().Substring(0, 8)}";
            Debug.Log($"Generated new GUID for checkpoint: {_id}");
        }


    }
}
