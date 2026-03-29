using Assets.Input;
using ECM2;
using System;

namespace Assets.Scripts.Player
{
    public class PlayerJumper : IDisposable
    {
        private Character _character;
        private readonly InputReader _inputReader;

        public PlayerJumper(Character character, InputReader inputReader)
        {
            _character = character;
            _inputReader = inputReader;

            //_inputReader.OnJumped += Jump;
            //_inputReader.OnJumpButtonUp += StopJump;
        }

        public void Dispose()
        {
            //_inputReader.OnJumped -= Jump;
            //_inputReader.OnJumpButtonUp -= StopJump;
        }

        public void Jump()
        {
            //_character.GetCharacterMovement().PauseGroundConstraint();
            _character.Jump();
            //_character.EnableGroundConstraint(true);
        }

        public void StopJump()
        {
            _character.StopJumping();
        }
    }
}
