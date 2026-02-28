using Assets.Input;
using ECM2;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerGravityController
    {
        private Vector3 _gravity = new Vector3(0, -10f, 0);

        private Character _character;
        private readonly InputReader _inputReader;
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;

        public PlayerGravityController(Character character, InputReader inputReader, 
            PlayerMoveDirectionCalculator playerMoveDirectionCalculator)
        {
            _character = character;
            _inputReader = inputReader;
            _playerMoveDirectionCalculator = playerMoveDirectionCalculator;

            //_playerMoveDirectionCalculator.SetGravity(_gravity);
        }




    }
}
