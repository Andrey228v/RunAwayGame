using Assets.Input;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerGravityController
    {
        private Vector3 _gravity = new Vector3(0, -10f, 0);

        private CharacterController _characterController;
        private readonly InputReader _inputReader;
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;

        public PlayerGravityController(CharacterController characterController, InputReader inputReader, 
            PlayerMoveDirectionCalculator playerMoveDirectionCalculator)
        {
            _characterController = characterController;
            _inputReader = inputReader;
            _playerMoveDirectionCalculator = playerMoveDirectionCalculator;

            _playerMoveDirectionCalculator.SetGravity(_gravity);
        }




    }
}
