using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Player
{
    public class PlayerRotator
    {
        private float _rotateSpeed = 200f;
        private CharacterController _characterController;
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;

        public PlayerRotator(CharacterController characterController, PlayerMoveDirectionCalculator playerMoveDirectionCalculator)
        {
            _characterController = characterController;
            _playerMoveDirectionCalculator = playerMoveDirectionCalculator;
        }

        //public void Tick()
        //{
        //    Rotate();
        //}

        public void Rotate()
        {
            if (_playerMoveDirectionCalculator.GetMoveDirection().magnitude > 0)
            {
                RotateDir(_playerMoveDirectionCalculator.GetMoveDirection(), _rotateSpeed);
            }
        }

        private void RotateDir(Vector3 direction, float rotateSpeed)
        {
            Debug.DrawRay(_characterController.transform.position, direction * 3f, Color.white, 1f);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            Quaternion q = new Quaternion(0f, targetRotation.y, 0f, targetRotation.w);
            _characterController.transform.rotation = Quaternion.RotateTowards(_characterController.transform.rotation, q, rotateSpeed * Time.deltaTime);
        }
    }
}
