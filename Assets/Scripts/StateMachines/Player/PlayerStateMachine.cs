using Assets.Input;
using Assets.Scripts.Player;
using Assets.Scripts.StateMachines.Player.States;
using ECM2;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.StateMachines.Player
{
    public class PlayerStateMachine : IStateSwitcher, ITickable, IFixedTickable, IStartable
    {
        private List<IState> _states = new List<IState>();
        private IState _currentState;
        private IState _prevState;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private InputReader _inputReader;
        private PlayerGroundChecker _playerGroundChecker;
        private PlayerJumper _playerJumper;
        private Character _character;
        private AnimatorController _animatorController;

        public PlayerStateMachine(PlayerMovement playerMovement, PlayerRotator playerRotator, 
            InputReader inputReader,
            PlayerGroundChecker playerGroundChecker,
            PlayerJumper playerJumper, Character character, AnimatorController animatorController)
        {
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _inputReader = inputReader;
            _playerGroundChecker = playerGroundChecker;
            _playerJumper = playerJumper;
            _character = character;
            _animatorController = animatorController;

        }

        public void Start()
        {
            Debug.Log(_character);

            _states = new List<IState>()
            {
                new MoveState(this, _playerMovement, _playerRotator, _inputReader, _animatorController),
                new JumpState(this, _playerMovement, _playerRotator, _playerGroundChecker, _playerJumper, _animatorController)
            };

            _currentState = _states[0];
            _prevState = _states[0];
            _currentState.Enter();
        }

        public void Tick()
        {

        }

        public void FixedTick()
        {
            _currentState.FixedUpdate();
            _currentState.CheckChangeState();
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
