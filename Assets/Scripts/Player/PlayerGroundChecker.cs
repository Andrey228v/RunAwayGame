using ECM2;

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
            return _character.IsGrounded();
        }
    }
}
