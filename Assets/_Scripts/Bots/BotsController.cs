using Assets._Scripts.Bots.BotStateMachine;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using ECM2;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using VContainer.Unity;

namespace Assets._Scripts.Bots
{
    //Класс, который хранит в себе всех ботом и запускает их StateMachine. Контролирует их количество, и создаёт/уничтожает.
    public class BotsController : IFixedTickable
    {
        private List<Bot> _bots = new List<Bot>();
        private BotFactory _botFactory;
        private GamePoints _gamePoints;
        private SaveLoadService _saveLoadService;

        public BotsController(BotFactory botFactory, GamePoints gamePoints, SaveLoadService saveLoadService)
        {
            _botFactory = botFactory;
            _gamePoints = gamePoints;
            _saveLoadService = saveLoadService;
        }

        public void FixedTick()
        {
            foreach (Bot bot in _bots) 
            {
                bot.FixedUpdateSM();
            }
        }

        public void AddBotsTransformCollection(BotTransformList botTransformList)
        {
            Transform botParent;

            if (botTransformList != null)
                botParent = botTransformList.Bots;
            else
                throw new ArgumentNullException(nameof(botTransformList), "botTransformList parent cannot be null");

            for (int i = 0; i < botParent.childCount; i++)
            {
                NavMeshCharacter agent = botParent.GetChild(i).GetComponent<NavMeshCharacter>();

                Bot bot = _botFactory.CreateBot(agent, _gamePoints);

                BotMB botMB = agent.gameObject.GetComponent<BotMB>();
                botMB.OnDie += DieRestartEntery;


                AddBot(bot);
            }
        }

        public void AddBot(Bot bot) 
        {
            _bots.Add(bot);
        }

        public void RemoveBot(Bot bot) 
        {
            _bots.Remove(bot);
        }

        private void DieRestartEntery(BotMB botMB)
        {

            LevelData levelData = _saveLoadService.GetLevelData();
            botMB.transform.SetLocalPositionAndRotation(levelData.LastCheckPointPosition, levelData.PlayerData.PlayerRotation);

        }
    }
}
