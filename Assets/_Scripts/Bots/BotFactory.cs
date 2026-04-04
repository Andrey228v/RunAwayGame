using Assets._Scripts.GameControllers;
using Assets.Scripts.Player;
using ECM2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Bots
{
    public class BotFactory : IDisposable, IRestart
    {
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private PlayerGroundChecker _playerGroundChecker;
        private PlayerJumper _playerJumper;
        private AnimatorController _animatorController;
        private FallController _fallController;

        public BotFactory(Character character, BotAIReader botAIReader)
        {


        }


        public void Dispose()
        {

        }

        public void Restart()
        {
            _animatorController.Restart();
        }
    }
}
