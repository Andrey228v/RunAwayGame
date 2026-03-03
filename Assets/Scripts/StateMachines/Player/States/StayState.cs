using Assets.Input;
using Assets.Scripts.Player;
using System;

namespace Assets.Scripts.StateMachines.Player.States
{
    public class StayState : IState, IDisposable
    {
        private readonly IStateSwitcher _stateSwitcher;
        private InputReader _inputReader;
        private AnimatorController _animatorController;
        private FallController _fallController;
        private bool _isFall = false;

        public StayState(IStateSwitcher stateSwitcher, InputReader inputReader, 
            AnimatorController animatorController, FallController fallController) 
        {
            _stateSwitcher = stateSwitcher;
            _inputReader = inputReader;
            _animatorController = animatorController;
            _fallController = fallController;
        }

        public void Dispose()
        {
            _inputReader.OnStartMove -= ChangeMoveState;
            _inputReader.OnJumped -= ChangeJumpState;
        }


        public void Enter()
        {
            _inputReader.OnStartMove += ChangeMoveState;
            _inputReader.OnJumped += ChangeJumpState;
            _animatorController.SetStatic(true);
            _animatorController.SetGround(true);
        }

        public void FixedUpdate()
        {
            _isFall = _fallController.GetIsFall();
        }

        public void Exit()
        {
            _inputReader.OnStartMove -= ChangeMoveState;
            _inputReader.OnJumped -= ChangeJumpState;
        }

        public void CheckChangeState()
        {
            if (_isFall)
            {
                _stateSwitcher.ChangeState<FallState>();
            }
        }

        public void ChangeMoveState()
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
