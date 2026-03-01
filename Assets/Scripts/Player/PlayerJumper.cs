using Assets.Input;
using ECM2;
using System;

namespace Assets.Scripts.Player
{
    public class PlayerJumper : IDisposable
    {
        private Character _character;
        private readonly InputReader _inputReader;
        private PlayerMoveDirectionCalculator _playerMoveDirectionCalculator;

        public PlayerJumper(Character character, InputReader inputReader, PlayerMoveDirectionCalculator playerMoveDirectionCalculator)
        {
            _character = character;
            _inputReader = inputReader;
            _playerMoveDirectionCalculator = playerMoveDirectionCalculator;

            _inputReader.OnJumped += Jump;
            _inputReader.OnJumpButtonUp += StopJump;
        }

        public void Dispose()
        {
            _inputReader.OnJumped -= Jump;
            _inputReader.OnJumpButtonUp -= StopJump;
        }

        public void Jump()
        {
            //_character.GetCharacterMovement().SetMovementMode(MovementMode.Flying);

            //Вопрос правильно ли это - хрен знает....
            // 1. Снимаем привязку к земле!
            //_character.GetCharacterMovement().PauseGroundConstraint();
            
            _character.Jump();
            //_character.LaunchCharacter();
            // 2. Возвращаем.
            //_character.EnableGroundConstraint(true);
        }

        public void StopJump()
        {
            _character.StopJumping();
        }
    }
}
