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

        public void Load(LevelData data)
        {
            _character.transform.SetLocalPositionAndRotation(data.PlayerData.PlayerPosition, data.PlayerData.PlayerRotation);
        }

        public void Save(LevelData data)
        {
            data.PlayerData.PlayerPosition = _character.transform.position;
            data.PlayerData.PlayerRotation = _character.transform.rotation;
        }

        public void Restart()
        {
            if (_character != null)
            {
                _character.SetVelocity(Vector3.zero);
                _character.StopJumping();
            }


        }

        public void FinishGame()
        {
            if (_character != null)
            {
                _character.SetVelocity(Vector3.zero);
                _character.StopJumping();
            }
        }
    }
}
