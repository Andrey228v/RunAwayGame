using Assets.Input;
using Assets.Scripts.Player;
using System;


namespace Assets.Scripts.StateMachines.Player.States
{
    public class JumpState : IState, IDisposable
    {
        private IStateSwitcher _stateSwitcher;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private PlayerGroundChecker _playerGroundChecker;
        private AnimatorController _animatorController;
        private FallController _fallController;
        private PlayerJumper _playerJumper;

        private bool _isGround = false;
        private bool _isFall = false;

        public JumpState(IStateSwitcher stateSwitcher, PlayerMovement playerMovement, 
            PlayerRotator playerRotator, PlayerGroundChecker playerGroundChecker,
            AnimatorController animatorController,
            FallController fallController, PlayerJumper playerJumper)
        {
            _stateSwitcher = stateSwitcher;
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _playerGroundChecker = playerGroundChecker;
            _animatorController = animatorController;
            _fallController = fallController;
            _playerJumper = playerJumper;
        }

        public void Dispose()
        {

        }

        public void Enter()
        {
            _isGround = false;
            _animatorController.SetJump(true);
            _animatorController.SetFall(false);
            _animatorController.SetGround(false);
            _playerJumper.StopJump();
        }

        public void FixedUpdate()
        {
            _playerMovement.Move();
            _playerRotator.Rotate();
            _isFall = _fallController.GetIsFall();
            _isGround = _playerGroundChecker.GetIsGrounded();
        }

        public void Exit()
        {
            _animatorController.SetJump(false);
            _playerJumper.StopJump();
        }

        public void CheckChangeState()
        {
            _isGround = _playerGroundChecker.GetIsGrounded();

            if (_isGround && _playerMovement.IsMove)
            {
                _stateSwitcher.ChangeState<MoveState>();
            }
            else if (_isGround && _playerMovement.IsMove == false)
            {
                _stateSwitcher.ChangeState<StayState>();
            }
            else if (_isGround == false && _isFall)
            {
                _stateSwitcher.ChangeState<FallState>();
            }
        }
    }
}
