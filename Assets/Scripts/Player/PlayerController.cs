using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using VContainer.Unity;

namespace Assets.Scripts.Player
{
    //ISaveLoad, IFixedTickable
    public class PlayerController : IFixedTickable, ISaveLoad
    {
        private PlayerStateMachine _playerStateMachine;
        private Character _character;


        public PlayerController()
        {
           
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

        public void Load(LevelData data)
        {
            _character.transform.SetLocalPositionAndRotation(data.PlayerData.PlayerPosition, data.PlayerData.PlayerRotation);
        }

        public void Save(LevelData data)
        {
            //_playerData.PlayerPosition = _character.position;
            //_playerData.PlayerRotation = _character.rotation;

            

            //_saveSystem.Save("PlayerData", _playerData); // Надо поменять ???
        }
    }
}
