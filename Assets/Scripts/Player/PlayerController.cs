using Assets.Scripts.StateMachines.Player;
using UnityEngine;

namespace Assets.Scripts.Player
{
    //ISaveLoad, IFixedTickable
    public class PlayerController : MonoBehaviour
    {
        private PlayerStateMachine _playerStateMachine;

        private void FixedUpdate()
        {
            _playerStateMachine.FixedTick();
        }

        public void SetPlayerStateMachine(PlayerStateMachine playerStateMachine)
        {
            _playerStateMachine = playerStateMachine;
        }

        public void Load()
        {
            //if (_saveSystem.HasKey("PlayerData"))
            //{
            //    var load = _saveSystem.Load<PlayerData>("PlayerData");
            //    _character.transform.SetPositionAndRotation(load.PlayerPosition, load.PlayerRotation);
            //}
        }

        public void Save()
        {
            //_playerData.PlayerPosition = _character.position;
            //_playerData.PlayerRotation = _character.rotation;
            //_saveSystem.Save("PlayerData", _playerData);
        }
    }
}
