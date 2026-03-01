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

        private bool _isGround = false;

        public JumpState(IStateSwitcher stateSwitcher, PlayerMovement playerMovement, 
            PlayerRotator playerRotator, PlayerGroundChecker playerGroundChecker,
            PlayerJumper playerJumper, AnimatorController animatorController,
            InputReader inputReader)
        {
            _stateSwitcher = stateSwitcher;
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _playerGroundChecker = playerGroundChecker;
            _playerJumper = playerJumper;
            _animatorController = animatorController;
            _inputReader = inputReader;
        }

        public void Dispose()
        {

        }

        public void Enter()
        {
            //_playerJumper.Jump();
            //Jump();
            //_inputReader.OnJumped += Jump;
            //_playerGroundChecker.PauseGroundChecking();
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
            //_isGround = _playerGroundChecker.GetIsGrounded();
        }

        public void LateUpdate()
        {
            _isGround = _playerGroundChecker.GetIsGrounded();
        }

        public void Exit()
        {
            //_playerJumper.StopJump();
            _animatorController.SetJump(false);
            //_inputReader.OnJumped -= Jump;
        }

        public void CheckChangeState()
        {
            //await UniTask.NextFrame();
            _isGround = _playerGroundChecker.GetIsGrounded();

            if (_isGround && _playerMovement.IsMove)
            {
                _stateSwitcher.ChangeState<MoveState>();
            }
            else if (_isGround && _playerMovement.IsMove == false)
            {
                _stateSwitcher.ChangeState<StayState>();
            }
        }

        //private void Jump()
        //{
        //    _playerJumper.Jump(); Debug.Log("TEST JUMP");
        //}
    }
}
