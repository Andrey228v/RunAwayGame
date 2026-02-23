using Assets.Input;
using Assets.Scripts.Player;
using Assets.Scripts.StateMachines.Player.States;
using System.Collections.Generic;
using System.Linq;
using VContainer.Unity;

namespace Assets.Scripts.StateMachines.Player
{
    public class PlayerStateMachine : IStateSwitcher, ITickable
    {
        private List<IState> _states = new List<IState>();
        private IState _currentState;
        private IState _prevState;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private InputReader _inputReader;

        public PlayerStateMachine(PlayerMovement playerMovement, PlayerRotator playerRotator, InputReader inputReader)
        {
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _inputReader = inputReader;

            _states = new List<IState>()
            {
                new MoveState(this, _playerMovement, _playerRotator, _inputReader),
                new JumpState()
            };
            
            _currentState = _states[0];
            _prevState = _states[0];
            _currentState.Enter();
        }

        public void Tick()
        {
            _currentState.Update();
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
