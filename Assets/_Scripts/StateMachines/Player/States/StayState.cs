using Assets.Input;
using Assets.Scripts.Player;
using System;

namespace Assets.Scripts.StateMachines.Player.States
{
    public class StayState : IState, IDisposable
    {
        private readonly IStateSwitcher _stateSwitcher;
        private AnimatorController _animatorController;
        private FallController _fallController;
        private PlayerGroundChecker _playerGroundChecker;
        private PlayerMovement _playerMovement;

        private bool _isFall = false;
        private bool _isMove = false;
        private bool _isGround = false;

        public StayState(IStateSwitcher stateSwitcher, 
            AnimatorController animatorController, 
            FallController fallController, PlayerGroundChecker playerGroundChecker,
            PlayerMovement playerMovement) 
        {
            _stateSwitcher = stateSwitcher;
            _animatorController = animatorController;
            _fallController = fallController;
            _playerGroundChecker = playerGroundChecker;
            _playerMovement = playerMovement;
        }

        public void Dispose()
        {
            //_inputReader.OnStartMove -= ChangeMoveState;
            //_inputReader.OnJumped -= ChangeJumpState;
        }


        public void Enter()
        {
            //_inputReader.OnStartMove += ChangeMoveState;
            //_inputReader.OnJumped += ChangeJumpState;
            _animatorController.SetStatic(true);
            _animatorController.SetGround(true);
        }

        public void FixedUpdate()
        {
            _isFall = _fallController.GetIsFall();
            _isMove = _playerMovement.IsMove;
            _isGround = _playerGroundChecker.GetIsGrounded();

        }

        public void Exit()
        {
            //_inputReader.OnStartMove -= ChangeMoveState;
            //_inputReader.OnJumped -= ChangeJumpState;
        }

        public void CheckChangeState()
        {
            if (_isFall)
            {
                _stateSwitcher.ChangeState<FallState>();
            }
            else if (_isMove)
            {
                ChangeMoveState();
            }
            else if(_isGround == false)
            {
                ChangeJumpState();
            }
        }

        private void ChangeMoveState()
        {
            _animatorController.SetStatic(false);
            _stateSwitcher.ChangeState<MoveState>();
        }

        private void ChangeJumpState()
        {
            _stateSwitcher.ChangeState<JumpState>();
        }
    }
}
