using Assets.Scripts.SaveLoad;
using ECM2;

namespace Assets.Scripts.Player
{
    public class PlayerController : ISaveLoad
    {
        private Character _character;
        private PlayerData _playerData;
        private ISaveSystem _saveSystem;

        public PlayerController(Character character, PlayerData playerData, ISaveSystem saveSystem) 
        {
            _character = character;
            _playerData = playerData;
            _saveSystem = saveSystem;
        }


        public void Load()
        {
            if (_saveSystem.HasKey("PlayerData"))
            {
                var load = _saveSystem.Load<PlayerData>("PlayerData");
                _character.transform.SetPositionAndRotation(load.PlayerPosition, load.PlayerRotation);
            }
        }

        public void Save()
        {
            _playerData.PlayerPosition = _character.position;
            _playerData.PlayerRotation = _character.rotation;
            _saveSystem.Save("PlayerData", _playerData);
        }
    }
}
