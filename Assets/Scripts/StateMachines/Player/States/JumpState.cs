using Assets.Input;
using Assets.Scripts.Player;
using Cysharp.Threading.Tasks;
using ECM2;
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

        private bool _isGround = false;

        public JumpState(IStateSwitcher stateSwitcher, PlayerMovement playerMovement, 
            PlayerRotator playerRotator, PlayerGroundChecker playerGroundChecker,
            PlayerJumper playerJumper, AnimatorController animatorController)
        {
            _stateSwitcher = stateSwitcher;
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _playerGroundChecker = playerGroundChecker;
            _playerJumper = playerJumper;
            _animatorController = animatorController;
        }

        public void Dispose()
        {

        }

        public void Enter()
        {
            _playerJumper.Jump();
            _animatorController.SetJump(true);
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
            _playerJumper.StopJump();
            _animatorController.SetJump(false);
        }

        public async void CheckChangeState()
        {
            await UniTask.NextFrame();
            _isGround = _playerGroundChecker.GetIsGrounded();
            Debug.Log(_isGround);

            if (_isGround)
            {
                ChangeStateMove();
            }
        }

        private void ChangeStateMove()
        {
            _stateSwitcher.ChangeState<MoveState>();
        }
    }
}
