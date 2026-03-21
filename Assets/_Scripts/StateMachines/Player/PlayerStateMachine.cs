using Assets.Input;
using Assets.Scripts.Player;
using Assets.Scripts.StateMachines.Player.States;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.StateMachines.Player
{
    public class PlayerStateMachine : IStateSwitcher
    {
        private List<IState> _states = new List<IState>();
        private IState _currentState;
        private IState _prevState;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private InputReader _inputReader;
        private PlayerGroundChecker _playerGroundChecker;
        private PlayerJumper _playerJumper;
        private AnimatorController _animatorController;
        private FallController _fallController;

        public PlayerStateMachine(PlayerMovement playerMovement, PlayerRotator playerRotator, InputReader inputReader,
            PlayerGroundChecker playerGroundChecker,
            PlayerJumper playerJumper, AnimatorController animatorController, FallController fallController)
        {
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _inputReader = inputReader;
            _playerGroundChecker = playerGroundChecker;
            _playerJumper = playerJumper;
            _animatorController = animatorController;
            _fallController = fallController;

            Start();
        }

        public void Start()
        {
            _states = new List<IState>()
            {
                new StayState(this, _inputReader, _animatorController, _fallController),
                new MoveState(this, _playerMovement, _playerRotator, _inputReader, _animatorController, _fallController),
                new JumpState(this, _playerMovement, _playerRotator, _playerGroundChecker, _animatorController, _fallController),
                new FallState(this, _playerMovement, _playerRotator, _inputReader, _animatorController, _playerGroundChecker, _fallController),
            };

            _currentState = _states[0];
            _prevState = _states[0];
            _currentState.Enter();
        }


        public void FixedTick()
        {
            _currentState.CheckChangeState();
            _currentState.FixedUpdate();
        }

        public void ChangeState<T>() where T : IState
        {
            var state = _states.FirstOrDefault(state => state is T);
            _prevState = _currentState;
            _currentState.Exit();
            _currentState = state;
            _currentState?.Enter();
        }
    }
}
