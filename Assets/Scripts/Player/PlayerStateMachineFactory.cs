using Assets.Input;
using Assets.Scripts.Camera;
using Assets.Scripts.StateMachines.Player;
using ECM2;

namespace Assets.Scripts.Player
{
    public class PlayerStateMachineFactory
    {

        public PlayerStateMachine Create(Character character, CameraController cameraController)
        {
            InputReader inputReader = new InputReader();
            PlayerMoveDirectionCalculator playerMoveDirectionCalculator = new PlayerMoveDirectionCalculator(cameraController, inputReader);
            PlayerMovement playerMovement = new PlayerMovement(character, inputReader, playerMoveDirectionCalculator);
            PlayerRotator playerRotator = new PlayerRotator(character, playerMoveDirectionCalculator);
            PlayerGroundChecker playerGroundChecker = new PlayerGroundChecker(character);
            PlayerJumper playerJumper = new PlayerJumper(character, inputReader);
            AnimatorController animatorController = new AnimatorController(character.animator);
            FallController fallController = new FallController(character);

            PlayerStateMachine playerStateMachine = new PlayerStateMachine(playerMovement, playerRotator, inputReader, playerGroundChecker, playerJumper, animatorController, fallController);

            return playerStateMachine;
        }
    }
}
