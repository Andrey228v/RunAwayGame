using ECM2;
using System;

namespace Assets.Scripts.Player
{
    public class PlayerGroundChecker : IDisposable
    {
        private Character _character;

        public PlayerGroundChecker(Character character)
        {
            _character = character;
        }

        public void Dispose()
        {
            _character = null;
        }

        public bool GetIsGrounded()
        {
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
