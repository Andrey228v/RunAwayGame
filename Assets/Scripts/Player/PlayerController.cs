using Assets.Scripts.StateMachines.Player;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Player
{
    //ISaveLoad, IFixedTickable
    public class PlayerController : ITickable, IFixedTickable
    {
        private PlayerStateMachine _playerStateMachine;


        public void Tick()
        {
            _playerStateMachine.Update();
        }

        public void FixedTick()
        {
            Debug.Log("TICK");
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
