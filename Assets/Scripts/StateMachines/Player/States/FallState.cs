

using Assets.Input;
using Assets.Scripts.Player;

namespace Assets.Scripts.StateMachines.Player.States
{
    public class FallState : IState
    {
        private readonly IStateSwitcher _stateSwitcher;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private InputReader _inputReader;
        private AnimatorController _animatorController;
        private PlayerGroundChecker _playerGroundChecker;
        private FallController _fallController;

        private bool _isGround;
        private bool _isFall;

        public FallState(IStateSwitcher stateSwitcher, PlayerMovement playerMovement,
            PlayerRotator playerRotator, InputReader inputReader, AnimatorController animatorController,
            PlayerGroundChecker playerGroundChecker, FallController fallController) 
        {
            _stateSwitcher = stateSwitcher;
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _inputReader = inputReader;
            _animatorController = animatorController;
            _playerGroundChecker = playerGroundChecker;
            _fallController = fallController;
        }


        public void Enter()
        {
            _animatorController.SetFall(true);
        }

        public void Update()
        {

        }

        public void FixedUpdate()
        {
            _playerMovement.Move();
            _playerRotator.Rotate();
            _isGround = _playerGroundChecker.GetIsGrounded();
            _isFall = _fallController.GetIsFall();
        }

        public void LateUpdate()
        {

        }

        public void Exit()
        {
            _animatorController.SetFall(false);
        }

        public void CheckChangeState()
        {
            if(_isGround && _playerMovement.IsMove == false)
                _stateSwitcher.ChangeState<StayState>();
            else if(_isGround && _playerMovement.IsMove)
                _stateSwitcher.ChangeState<MoveState>();
            else if (_isFall == false && _isGround == false)
                _stateSwitcher.ChangeState<JumpState>();
        }
    }
}
