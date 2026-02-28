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
    public class PlayerStateMachine : IStateSwitcher, ITickable, IFixedTickable
    {
        private List<IState> _states = new List<IState>();
        private IState _currentState;
        private IState _prevState;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private InputReader _inputReader;
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;
        private PlayerGroundChecker _playerGroundChecker;
        private PlayerGravityController _playerGravityController;
        private PlayerJumper _playerJumper;
        private Character _character;

        public PlayerStateMachine(PlayerMovement playerMovement, PlayerRotator playerRotator, 
            InputReader inputReader, PlayerMoveDirectionCalculator playerMoveDirectionCalculator,
            PlayerGroundChecker playerGroundChecker, PlayerGravityController playerGravityController,
            PlayerJumper playerJumper, Character character)
        {
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _inputReader = inputReader;
            _playerMoveDirectionCalculator = playerMoveDirectionCalculator;
            _playerGroundChecker = playerGroundChecker;
            _playerGravityController = playerGravityController;
            _playerJumper = playerJumper;
            _character = character;

            _states = new List<IState>()
            {
                new MoveState(this, _playerMovement, _playerRotator, _inputReader),
                new JumpState(this, _inputReader, _playerMoveDirectionCalculator, _playerMovement, _playerRotator, _playerGroundChecker, _playerGravityController, _playerJumper)
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
