using Assets._Scripts.ObjectsScripts.StateMachines.Player;
using Assets._Scripts.SaveLoad.Data.Interfaces;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using ECM2;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.ObjectsScripts.Player
{
    public class PlayerController : IFixedTickable, ISave, ILoad, IDieRestart, IFinish, IReset
    {
        private UnitStateMachine _playerStateMachine;
        private Character _character;
        private PlayerMB _playerMB;
        private bool _isDisposed = false;

        public PlayerMB PlayerMB => _playerMB;

        public void Initialization(LevelData levelData)
        {
            if(levelData.PlayerData == null)
            {
                levelData.PlayerData = new PlayerData();
            }
        }

        public void Dispose()
        {
            _isDisposed = true;

            _playerStateMachine.Dispose();

            _playerStateMachine = null;
            _character = null;
        }

        public void FixedTick()
        {
            if (_isDisposed) return;
            _playerStateMachine.FixedTick();
        }

        public void SetPlayerStateMachine(UnitStateMachine playerStateMachine)
        {
            _playerStateMachine = playerStateMachine;
        }

        public void SetCharacter(Character character)
        {
            _character = character;
        }

        public void SetPlayerMB(PlayerMB playerMB)
        {
            _playerMB = playerMB;
        }

        public void Finish(LevelData levelData)
        {
            Reset();
            _character.transform.SetLocalPositionAndRotation(levelData.LastCheckPointPosition.position, levelData.LastCheckPointPosition.rotation);
        }

        public void DieRestart(LevelData levelData)
        {
            Reset();
            _playerMB.transform.SetLocalPositionAndRotation(levelData.LastCheckPointPosition.position, levelData.LastCheckPointPosition.rotation);
        }

        public void Save(LevelData levelData)
        {
            levelData.PlayerData.PlayerPosition = _character.transform.position;
            levelData.PlayerData.PlayerRotation = _character.transform.rotation;
        }

        public void Load(LevelData levelData)
        {

            if (levelData.PlayerData == null)
            {
                var playerData = new PlayerData
                {
                    PlayerPosition = levelData.PlayerData.PlayerPosition,
                    PlayerRotation = levelData.PlayerData.PlayerRotation
                };

                levelData.PlayerData = playerData;
            }

            _character.transform.SetLocalPositionAndRotation(levelData.LastCheckPointPosition.position, levelData.LastCheckPointPosition.rotation); // при финише надо ставить точку старта
        }

        private void Reset()
        {
            if (_playerStateMachine != null)
            {
                _playerStateMachine.Restart();
            }

            if (_character != null)
            {
                _character.SetVelocity(Vector3.zero);
                _character.StopJumping();

                _character.SetMovementMode(Character.MovementMode.Falling);
            }
        }

        public void ResetAllObjects(LevelConfig config)
        {
            
        }
    }
}
