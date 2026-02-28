using Assets.Input;
using Assets.Scripts.Player;
using System;

namespace Assets.Scripts.StateMachines.Player.States
{
    public class MoveState : IState, IDisposable
    {
        private readonly IStateSwitcher _stateSwitcher;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private InputReader _inputReader;

        public MoveState(IStateSwitcher stateSwitcher, PlayerMovement playerMovement,
            PlayerRotator playerRotator, InputReader inputReader)
        {
            _stateSwitcher = stateSwitcher;
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _inputReader = inputReader;

            _inputReader.OnJumped += ChangeStateJump;
        }

        public void Dispose()
        {
            _inputReader.OnJumped -= ChangeStateJump;
        }

        public void Enter()
        {

        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            _playerMovement.Move();
            _playerRotator.Rotate();
        }

        public void Exit()
        {

        }

        public void CheckChangeState()
        {

        }

        private void ChangeStateJump()
        {
            _stateSwitcher.ChangeState<JumpState>();
        }
    }
}
