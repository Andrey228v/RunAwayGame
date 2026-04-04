using Assets.Scripts.Camera;
using Assets.Scripts.Player;
using TMPro;
using UnityEngine;

namespace Assets._Scripts.UI
{
    public class UnitInfoUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _gobeletsCount;

        public UnitInfoUI(string name, int count)
        {
            _name.text = name;
            _gobeletsCount.text = count.ToString();
        }

        public void RotateToCamera(Vector3 direction)
        {
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = targetRotation;
            }
        }

    }
}
