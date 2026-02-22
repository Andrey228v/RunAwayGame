using Assets.Input;
using UnityEngine;
using VContainer;

namespace Assets.Scripts.Player
{
    public class PlayerMovement
    {
        private CharacterController _characterController;
        private readonly InputReader _inputReader;

        public PlayerMovement(CharacterController characterController)
        {
            _inputReader = new InputReader();
            _characterController = characterController;
            _inputReader.OnDirectionMoveChandged += Move;

            Debug.Log("INIT");
        }
        public void Move(Vector2 direction)
        {
            Vector3 move = new Vector3(direction.x, 0, direction.y);
            _characterController.Move(move * 10f * Time.deltaTime);
            Debug.Log($"dir:{direction}");
        }
        

    }
}
