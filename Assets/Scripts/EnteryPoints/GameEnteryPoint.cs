using Assets.Scripts.Player;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.EnteryPoints
{
    public class GameEnteryPoint : MonoBehaviour
    {
        private Test _test;
        private PlayerMovement _playerMovement;

        [Inject]
        public void Constructor(Test test, PlayerMovement playerMovement)
        {
            _test = test;
            _playerMovement = playerMovement;
        }

    }
}
