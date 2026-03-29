using Assets._Scripts.GameControllers;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Player
{
    public class PlayerController : IFixedTickable, ISaveLoad, IDisposable, IRestart, IFinish
    {
        private PlayerStateMachine _playerStateMachine;
        private Character _character;

        public void Dispose()
        {
            Debug.Log("PLAYER CONTROLLER DESTROU");
            _playerStateMachine.Dispose();

            _playerStateMachine = null;
            _character = null;
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

        public void Load(LevelData levelData, LevelConfig levelConfig)
        {
            if (levelData.PlayerData == null)
            {
                var playerData = new PlayerData
                {
                    PlayerPosition = levelConfig.StartPosition,
                    PlayerRotation = Quaternion.Euler(levelConfig.StartRotationEuler),
                };

                levelData.PlayerData = playerData;
            }

            _character.transform.SetLocalPositionAndRotation(levelData.PlayerData.PlayerPosition, levelData.PlayerData.PlayerRotation);
        }

        public void Save(LevelData data)
        {
            data.PlayerData.PlayerPosition = _character.transform.position;
            data.PlayerData.PlayerRotation = _character.transform.rotation;
        }

        public void Restart()
        {
            if(_playerStateMachine != null)
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

        public void FinishGame()
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
    }
}
