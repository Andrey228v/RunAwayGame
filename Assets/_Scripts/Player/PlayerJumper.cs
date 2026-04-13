using Assets.Input;
using ECM2;
using System;

namespace Assets.Scripts.Player
{
    public class PlayerJumper : IDisposable
    {
        private Character _character;

        public PlayerJumper(Character character)
        {
            _character = character;
        }

        public void Dispose()
        {
        }

        public void Jump()
        {
            _character.Jump();
        }

        public void StopJump()
        {
            _character.StopJumping();
        }
    }
}
