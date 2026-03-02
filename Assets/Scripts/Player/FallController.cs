using ECM2;

namespace Assets.Scripts.Player
{
    public class FallController
    {
        private Character _character;

        public FallController(Character character)
        {
            _character = character;
        }

        public bool GetIsFall()
        {
            return _character.IsFalling();
        }

    }
}
