using System;
using Unity.Cinemachine;
using UnityEngine;

namespace Assets.Scripts.Camera
{
    //Orbital Follow = Lock To Target on Assign.
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _cameraCinemachine;

        private Transform _cameraTransform;
        private Quaternion _targetRotation;

        public event Action<Quaternion> OnCameraAngleChanged;

        public CinemachineCamera CameraCinemachine => _cameraCinemachine;

        public void Start()
        {
            _cameraTransform = _cameraCinemachine.transform;
            

        }

        public void FixedUpdate()
        {
            _targetRotation = Quaternion.AngleAxis(_cameraTransform.eulerAngles.y, Vector3.up);
            OnCameraAngleChanged?.Invoke(_targetRotation);
        }

        public void SetTarget(Transform target)
        {
            _cameraCinemachine.Follow = target;
        }
    }
}
