using Assets._Scripts.GameControllers;
using Assets._Scripts.SaveLoad.Service;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using Assets.Scripts.StateMachines.Player;
using ECM2;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Player
{
    public class PlayerController : IFixedTickable, IDisposable, IRestart, IFinish, IStartable, ISaveLoadService //ISaveLoad ISaveLoadService
    {
        private UnitStateMachine _playerStateMachine;
        private Character _character;
        private PlayerMB _playerMB;

        public PlayerMB PlayerMB => _playerMB;

        
        public void Start()
        {
            
        }

        public void Dispose()
        {
            _playerStateMachine.Dispose();

            _playerStateMachine = null;
            _character = null;
        }

        public void Initialize()
        {
            
        }

        public void FixedTick()
        {
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

        public void Restart()
        {
            Reset();
        }

        public void FinishGame()
        {
            Reset();
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

        public void SetPlayerMB(PlayerMB playerMB)
        {
            _playerMB = playerMB;
        }

        public void DieRestart(LevelData levelData)
        {
            Reset();

            _playerMB.transform.SetLocalPositionAndRotation(levelData.LastCheckPointPosition, levelData.PlayerData.PlayerRotation);
        }

        public void AddSerice(ISaveLoadService service)
        {
            
        }

        public void SaveAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            var levelData = gameSaveData.LevelsData[levelConfig.LevelName];

            levelData.PlayerData.PlayerPosition = _character.transform.position;
            levelData.PlayerData.PlayerRotation = _character.transform.rotation;
        }

        public void LoadAllServices(GameSaveData gameSaveData, LevelConfig levelConfig)
        {
            var levelData = gameSaveData.LevelsData[levelConfig.LevelName];
            //var config = gameSaveData.LevelConfig;

            if (levelData.PlayerData == null)
            {
                var playerData = new PlayerData
                {
                    PlayerPosition = levelConfig.StartPosition,
                    PlayerRotation = Quaternion.Euler(levelConfig.StartRotationEuler),
                };

                levelData.PlayerData = playerData;
                levelData.LastCheckPointPosition = levelConfig.StartPosition; // под вопросом.
            }

            _character.transform.SetLocalPositionAndRotation(levelData.LastCheckPointPosition, levelData.PlayerData.PlayerRotation);
        }




        //public void Load(LevelData levelData, LevelConfig levelConfig)
        //{
        //    Reset();

        //    if (levelData.PlayerData == null)
        //    {
        //        var playerData = new PlayerData
        //        {
        //            PlayerPosition = levelConfig.StartPosition,
        //            PlayerRotation = Quaternion.Euler(levelConfig.StartRotationEuler),
        //        };

        //        levelData.PlayerData = playerData;
        //        levelData.LastCheckPointPosition = levelConfig.StartPosition;
        //    }

        //    _character.transform.SetLocalPositionAndRotation(levelData.LastCheckPointPosition, levelData.PlayerData.PlayerRotation);
        //}

        //public void Save(LevelData data)
        //{
        //    data.PlayerData.PlayerPosition = _character.transform.position;
        //    data.PlayerData.PlayerRotation = _character.transform.rotation;
        //}
    }
}
