using Assets.Input;
using Assets.Scripts.Camera;
using System;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMoveDirectionCalculator : IDisposable
    {
        private CameraController _cameraController;
        private InputReader _inputReader;
        private Vector3 _inputDirection;
        private Quaternion _cameraAngleRotation;
        private Vector3 _direction;

        public PlayerMoveDirectionCalculator(CameraController cameraController, InputReader inputReader)
        {
            _cameraController = cameraController;
            _inputReader = inputReader;

            _inputReader.OnDirectionMoveChandged += SetInputDirection;
            _cameraController.OnCameraAngleChanged += SetAngleCameraRotation;
        }

        public void Dispose()
        {
            _inputReader.OnDirectionMoveChandged -= SetInputDirection;
            _cameraController.OnCameraAngleChanged -= SetAngleCameraRotation;
        }

        public Vector3 GetMoveDirection()
        {
            _direction = _cameraAngleRotation * _inputDirection;
            return _direction;
        }

        private void SetInputDirection(Vector2 direction)
        {
            _inputDirection = new Vector3(direction.x, 0, direction.y).normalized;
        }

        private void SetAngleCameraRotation(Quaternion rotation)
        {
            _cameraAngleRotation = rotation;
        }
    }
}
