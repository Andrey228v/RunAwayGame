using Assets.Scripts.Camera;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer.Unity;

namespace Assets.Scripts.Player
{
    public class PlayerRotator : ITickable
    {
        private float _rotateSpeed = 100f;
        private CharacterController _characterController;
        private CameraController _cameraController;

        public PlayerRotator(CharacterController characterController, CameraController cameraController)
        {
            _characterController = characterController;
            _cameraController = cameraController;
        }

        public void Tick()
        {
            //if (_moveDirection.magnitude > 0)
            //{
            //    CharacterView.RotateTowards(_moveDirection, _rotateSpeed);
            //}
        }
    }
}
