using Assets.Input;
using Assets.Scripts.Camera;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : IDisposable, ITickable
    {
        private CharacterController _characterController;
        private readonly InputReader _inputReader;
        private CameraController _cameraController;

        private bool _isMove;
        private Vector3 _inputDirection;
        private Vector3 _direction;
        private float _speed = 5f;
        private Quaternion _cameraAngleRotation;

        public PlayerMovement(CharacterController characterController, CameraController cameraController)
        {
            _inputReader = new InputReader();
            _characterController = characterController;
            _cameraController = cameraController;
            _isMove = false;

            _inputReader.OnDirectionMoveChandged += SetInputDirection;
            _inputReader.OnMoved += SetIsMove;
            _cameraController.OnCameraAngleChanged += SetAngleCameraRotation;;
        }

        public void Dispose()
        {
            _inputReader.OnDirectionMoveChandged -= SetInputDirection;
            _inputReader.OnMoved -= SetIsMove;
            _cameraController.OnCameraAngleChanged -= SetAngleCameraRotation;
        }

        public void Tick()
        {
            Move();

            if (_direction.magnitude > 0)
            {
                Rotate(_direction, 100f);
            }
        }

        private void Move()
        {
            if (_isMove)
            {
                _characterController.Move(_direction * _speed * Time.deltaTime);
            }
        }

        private void SetIsMove(bool isMove) 
        {
            _isMove = isMove;
        }

        private void SetDirection()
        {
            _direction = _cameraAngleRotation * _inputDirection;
        }

        private void SetInputDirection(Vector2 direction)
        {
            _inputDirection = new Vector3(direction.x, 0, direction.y).normalized;
        }

        private void SetAngleCameraRotation(Quaternion rotation)
        {
            _cameraAngleRotation = rotation;
            SetDirection();
        }

        private void Rotate(Vector3 direction, float rotateSpeed)
        {
            Debug.DrawRay(_characterController.transform.position, direction * 3f, Color.white, 1f);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion q = new Quaternion(0f, targetRotation.y, 0f, targetRotation.w);
            _characterController.transform.rotation = Quaternion.RotateTowards(_characterController.transform.rotation, q, rotateSpeed * Time.deltaTime);
        }
    }
}
