using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using Cysharp.Threading.Tasks;
using ECM2;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.Bots
{
    //Класс, который хранит в себе всех ботом и запускает их StateMachine. Контролирует их количество, и создаёт/уничтожает.
    public class BotsController : IFixedTickable, IStartable
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

        public async void Start()
        {
            CreateBot();
            await UniTask.Delay(2000);
            CreateBot();
        }

        public void FixedTick()
        {
            foreach (Bot bot in _bots) 
            {
                bot.FixedUpdateSM();
            }
        }


        //Сделать Пул объектов потом...
        public Bot CreateBot()
        {
            Bot bot = _botFactory.CreateBot();
            AddBot(bot);

            return bot;
        }

        public void AddBot(Bot bot) 
        {
            _bots.Add(bot);
        }

        public void RemoveBot(Bot bot) 
        {
            _bots.Remove(bot);
        }

        public void DieRestartEntery(BotMB botMB)
        {

            LevelData levelData = _saveLoadService.GetLevelData();
            botMB.transform.SetLocalPositionAndRotation(levelData.LastCheckPointPosition, levelData.PlayerData.PlayerRotation);

        }


    }
}
