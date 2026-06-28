using Assets.Scripts.UI;
using System;
using TMPro;
using UnityEngine;

namespace Assets._Scripts.UI
{
    public class UnitInfoUIView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _gobeletsCount;

        public event Action OnDestroyView;

        public UnitInfoUIView(string name, int count)
        {
            _name.text = name;
            _gobeletsCount.text = count.ToString();
        }

        private void OnDestroy()
        {
            OnDestroyView?.Invoke();
        }

        public void RotateToCamera(Vector3 direction)
        {
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation;
            }
        }

        public void SetGobeletsCountText(int actualGobelets, int addGobelets)
        {
            _gobeletsCount.text = actualGobelets.ToString();
        }
    }
}
