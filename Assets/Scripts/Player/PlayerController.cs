using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using System;
using VContainer.Unity;

namespace Assets.Scripts.Player
{
    public class PlayerController : IFixedTickable, ISaveLoad
    {
        private PlayerStateMachine _playerStateMachine;
        private Character _character;

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

        public void Load(LevelData data)
        {
            _character.transform.SetLocalPositionAndRotation(data.PlayerData.PlayerPosition, data.PlayerData.PlayerRotation);
        }

        public void Save(LevelData data)
        {
            data.PlayerData.PlayerPosition = _character.transform.position;
            data.PlayerData.PlayerRotation = _character.transform.rotation;
        }
    }
}
