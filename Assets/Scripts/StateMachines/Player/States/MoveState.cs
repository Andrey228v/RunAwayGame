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
        private AnimatorController _animatorController;
        private FallController _fallController;
        private bool _isFall = false;

        public MoveState(IStateSwitcher stateSwitcher, PlayerMovement playerMovement,
            PlayerRotator playerRotator, InputReader inputReader, AnimatorController animatorController, FallController fallController)
        {
            _stateSwitcher = stateSwitcher;
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _inputReader = inputReader;
            _animatorController = animatorController;
            _fallController = fallController;
        }

        public void Dispose()
        {
            _inputReader.OnStoped -= ChangeStayState;
            _inputReader.OnJumped -= ChangeJumpState;
        }

        public void Enter()
        {
            _inputReader.OnStoped += ChangeStayState;
            _inputReader.OnJumped += ChangeJumpState;
            _animatorController.SetMove(true);
        }

        public void Update()
        {
            
        }

        public void FixedUpdate()
        {
            _playerMovement.Move();
            _playerRotator.Rotate();
            _isFall = _fallController.GetIsFall();
        }

        public void LateUpdate()
        {

        }

        public void Exit()
        {
            _inputReader.OnStoped -= ChangeStayState;
            _inputReader.OnJumped -= ChangeJumpState;
        }

        public void CheckChangeState()
        {
            if (_isFall)
            {
                _stateSwitcher.ChangeState<FallState>();
            }
        }

        private void ChangeStayState()
        {
            _animatorController.SetMove(false);
            _playerMovement.Stop();
            _stateSwitcher.ChangeState<StayState>();
        }

        private void ChangeJumpState()
        {
            _stateSwitcher.ChangeState<JumpState>();
        }
    }
}
