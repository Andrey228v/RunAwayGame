
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerGroundChecker
    {
        private CharacterController _characterController;


        private bool _isGround;

        public PlayerGroundChecker(CharacterController characterController)
        {
            _characterController = characterController;
        }

        public bool GetIsGrounded()
        {
            _isGround = _characterController.isGrounded;
            return _isGround;
        }


    }
}
