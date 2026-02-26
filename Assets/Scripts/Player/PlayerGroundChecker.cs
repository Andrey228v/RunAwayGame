
using ECM2;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerGroundChecker
    {
        private Character _character;


        private bool _isGround;

        public PlayerGroundChecker(Character character)
        {
            _character = character;
        }

        public bool GetIsGrounded()
        {
            _isGround = _character.IsGrounded();
            return _isGround;
        }


    }
}
