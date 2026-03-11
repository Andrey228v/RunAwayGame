using Assets.Scripts.SaveLoad;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Player
{
    //ISaveLoad, IFixedTickable
    public class PlayerController : IFixedTickable, ISaveLoad
    {
        private PlayerStateMachine _playerStateMachine;
        private ISaveSystem _saveSystem;
        private Character _character;
        private PlayerData _playerData;

        public PlayerController(ISaveSystem saveSystem, PlayerData playerData)
        {
            _saveSystem = saveSystem;
            _playerData = playerData;
        }

        public void FixedTick()
        {
            _playerStateMachine.FixedTick();
        }

        public void SetPlayerStateMachine(PlayerStateMachine playerStateMachine)
        {
            _playerStateMachine = playerStateMachine;
        }

        public void SetCharacter(Character character)
        {
            _character = character;
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
            _playerData.PlayerPosition = _character.position;
            _playerData.PlayerRotation = _character.rotation;

            //_saveSystem.Save("PlayerData", _playerData); // Надо поменять ???
        }
    }
}
