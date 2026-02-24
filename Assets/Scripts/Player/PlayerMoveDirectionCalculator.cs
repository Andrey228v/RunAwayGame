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
        private Vector3 _jumpForce = Vector3.zero;
        private Vector3 _gravity = Vector3.zero;

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
            _direction = _cameraAngleRotation * (_inputDirection + _jumpForce + _gravity);
            _jumpForce = Vector3.zero;
            return _direction;
        }

        public void SetJumpForce(Vector3 force)
        {
            _jumpForce = force;
        }

        public void SetGravity(Vector3 force)
        {
            _gravity = force;
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
