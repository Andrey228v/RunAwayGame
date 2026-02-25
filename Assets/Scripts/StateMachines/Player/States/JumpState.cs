using Assets.Input;
using Assets.Scripts.Player;
using System;
using UnityEngine;

namespace Assets.Scripts.StateMachines.Player.States
{
    public class JumpState : IState, IDisposable
    {
        private int _forceX = 5;
        private int _forceY = 10;

        private IStateSwitcher _stateSwitcher;
        private readonly InputReader _inputReader;
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;
        private PlayerMovement _playerMovement;
        private PlayerRotator _playerRotator;
        private PlayerGroundChecker _playerGroundChecker;
        private PlayerGravityController _playerGravityController;

        private bool _isGround = false;

        public JumpState(IStateSwitcher stateSwitcher, InputReader inputReader, 
            PlayerMoveDirectionCalculator playerMoveDirectionCalculator, PlayerMovement playerMovement, 
            PlayerRotator playerRotator, PlayerGroundChecker playerGroundChecker, PlayerGravityController playerGravityController)
        {
            _stateSwitcher = stateSwitcher;
            _inputReader = inputReader;
            _playerMoveDirectionCalculator = playerMoveDirectionCalculator;
            _playerMovement = playerMovement;
            _playerRotator = playerRotator;
            _playerGroundChecker = playerGroundChecker;
        }

        public void Dispose()
        {

        }

        public void Enter()
        {
            _playerMoveDirectionCalculator.SetJumpForce(new Vector3(_forceX, _forceY, 0f));

            ChangeStateMove();
        }
        public void Update()
        {
            //_playerMovement.Move();
            //_playerRotator.Rotate();

            //_isGround = _playerGroundChecker.GetIsGrounded();

            //if (_isGround) 
            //{
            //    ChangeStateMove();
            //}
        }

        public void Exit()
        {
            
        }

        private void ChangeStateMove()
        {
            
            _stateSwitcher.ChangeState<JumpState>();
        }
    }
}
