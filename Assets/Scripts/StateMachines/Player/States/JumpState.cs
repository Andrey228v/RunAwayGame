using Assets.Input;
using Assets.Scripts.Player;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;


namespace Assets.Scripts.StateMachines.Player.States
{
    public class JumpState : IState, IDisposable
    {
        private IStateSwitcher _stateSwitcher;
        private PlayerJumper _playerJumper;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private PlayerGroundChecker _playerGroundChecker;
        private AnimatorController _animatorController;
        private InputReader _inputReader;
        private FallController _fallController;

        private bool _isGround = false;
        private bool _isFall = false;

        public JumpState(IStateSwitcher stateSwitcher, PlayerMovement playerMovement, 
            PlayerRotator playerRotator, PlayerGroundChecker playerGroundChecker,
            PlayerJumper playerJumper, AnimatorController animatorController,
            InputReader inputReader, FallController fallController)
        {
            _stateSwitcher = stateSwitcher;
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _playerGroundChecker = playerGroundChecker;
            _playerJumper = playerJumper;
            _animatorController = animatorController;
            _inputReader = inputReader;
            _fallController = fallController;
        }

        public void Dispose()
        {

        }

        public void Enter()
        {
            _isGround = false;
            _animatorController.SetJump(true);
        }
        public void Update()
        {

        }

        public void FixedUpdate()
        {
            _playerMovement.Move();
            _playerRotator.Rotate();
            _isFall = _fallController.GetIsFall();
            _isGround = _playerGroundChecker.GetIsGrounded();
        }

        public void LateUpdate()
        {
            
        }

        public void Exit()
        {
            _animatorController.SetJump(false);
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
