using ECM2;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerRotator
    {
        private Character _character;
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;

        public PlayerRotator(Character character, PlayerMoveDirectionCalculator playerMoveDirectionCalculator)
        {
            _character = character;
            _playerMoveDirectionCalculator = playerMoveDirectionCalculator;
        }

        public void Rotate()
        {
            if (_playerMoveDirectionCalculator.GetMoveDirection().magnitude > 0)
            {
                RotateDir(_playerMoveDirectionCalculator.GetMoveDirection(), Time.deltaTime);
            }
        }

        private void RotateDir(Vector3 direction, float rotateSpeed)
        {
            _character.RotateTowards(direction, rotateSpeed);
        }
    }
}
