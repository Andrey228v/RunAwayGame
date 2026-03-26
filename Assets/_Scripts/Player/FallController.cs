using ECM2;
using System;

namespace Assets.Scripts.Player
{
    public class FallController : IDisposable
    {
        private Character _character;

        public FallController(Character character)
        {
            _character = character;
        }

        public void Dispose()
        {
            _character = null;
        }

        public bool GetIsFall()
        {
            return _character.IsFalling() && _character.velocity.y < 0;
        }

    }
}
