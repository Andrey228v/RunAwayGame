using ECM2;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerGroundChecker
    {
        private Character _character;

        public PlayerGroundChecker(Character character)
        {
            _character = character;
        }

        public bool GetIsGrounded()
        {
            //IsGrounded = isOnWalkableGround && isConstrainedToGround;
            //IsOnGround = _currentGround.hitGround;
            //_character.GetCharacterMovement().PauseGroundConstraint();
            Debug.Log($"IsGrounded:{_character.IsGrounded()}, IsOnGround: {_character.IsOnGround()}, isOnGround: {_character.GetCharacterMovement().isOnGround}");

            return _character.IsGrounded(); 
        }

        public void PauseGroundChecking() 
        {
            _character.GetCharacterMovement().PauseGroundConstraint();
        }

        public void ReturnGroundChecking()
        {
            _character.EnableGroundConstraint(true);
        }
    }
}
