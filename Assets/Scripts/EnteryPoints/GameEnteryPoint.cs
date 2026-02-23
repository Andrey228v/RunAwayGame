using Assets.Input;
using Assets.Scripts.Player;
using Assets.Scripts.StateMachines.Player;
using Assets.Scripts.StateMachines.Player.States;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.EnteryPoints
{
    public class GameEnteryPoint : MonoBehaviour
    {
        private Test _test;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private PlayerStateMachine _playerStateMachine;
        private InputReader _inputReader;

        [Inject]
        public void Constructor(Test test, PlayerMovement playerMovement, PlayerRotator playerRotator, InputReader inputReader)
        {
            _test = test;
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _inputReader = inputReader;


        }

        private void Start()
        {
            _playerStateMachine = new PlayerStateMachine(_playerMovement, _playerRotator, _inputReader);
        }

    }
}
