using Assets.Input;
using Assets.Scripts.Player;

namespace Assets.Scripts.StateMachines.Player.States
{
    public class FallState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private AnimatorController _animatorController;
        private PlayerGroundChecker _playerGroundChecker;
        private FallController _fallController;

        private bool _isGround;
        private bool _isFall;
        private bool _isMove;

        public FallState(IStateSwitcher stateSwitcher, PlayerMovement playerMovement,
            PlayerRotator playerRotator, AnimatorController animatorController,
            PlayerGroundChecker playerGroundChecker, FallController fallController) 
        {
            _stateSwitcher = stateSwitcher;
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _animatorController = animatorController;
            _playerGroundChecker = playerGroundChecker;
            _fallController = fallController;
        }

        public void Enter()
        {
            _animatorController.SetFall(true);
            _animatorController.SetGround(false);
        }

        public void FixedUpdate()
        {
            _playerMovement.Move();
            _playerRotator.Rotate();
            _isGround = _playerGroundChecker.GetIsGrounded();
            _isFall = _fallController.GetIsFall();
            _isMove = _playerMovement.IsMove;

        }

        public void Exit()
        {
            _animatorController.SetFall(_isFall);
            _animatorController.SetGround(_isGround);
            _animatorController.SetMove(_isMove);
            _animatorController.SetStatic(!_isMove);
        }

        public void CheckChangeState()
        {
            if(_isGround && _isMove == false)
                _stateSwitcher.ChangeState<StayState>();
            else if(_isGround && _isMove)
                _stateSwitcher.ChangeState<MoveState>();
            else if (_isFall == false && _isGround == false)
                _stateSwitcher.ChangeState<JumpState>();
        }
    }
}
