using Assets._Scripts.Bots;
using Assets._Scripts.EnteryPoints.Interfaces;
using Assets._Scripts.GameControllers;
using Assets.Scripts.Player;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using Assets.Scripts.SaveLoad.Data;
using ECM2;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints.Level
{
    public class BotAIEnteryPoint : IStartable, IDisposable, IInitFinish
    {
        private BotTransformList _botList;
        private BotsController _botController;
        private SaveLoadService _saveLoadService;
        private BotFactory _botFactory;
        private Func<Character> _characterFactory;
        private GamePoints _gamePoints;

        public IEnumerable<IFinish> Finished { get; private set; }

        public BotAIEnteryPoint(BotsController botController, 
            IEnumerable<IFinish> fineshed, 
            BotTransformList botList, 
            SaveLoadService saveLoadService, 
            BotFactory botFactory, 
            Func<Character> characterFactory, GamePoints gamePoints)
        {
            _botController = botController;
            Finished = fineshed;
            _botList = botList;
            _saveLoadService = saveLoadService;
            _botFactory = botFactory;
            _characterFactory = characterFactory;
            _gamePoints = gamePoints;

        }

        public void Dispose()
        {

        }

        public void Start()
        {
            //1) Получить дату уровня.
            LevelData levelData = _saveLoadService.GetLevelData();

            //InitBots(levelData, _characterFactory);

        }

        //Переместить... функция должна создавать ботов и управлять их созданимем.
        private void InitBots(LevelData levelData, Func<Character> characterFactory)
        {
            //Создать...
            //int botsCount = 2;

            //for (int i = 0; i < botsCount; i++)
            //{
            //    Character character = characterFactory();
            //    character.AddComponent<NavMeshCharacter>(); // Тут подумать так ли делать ...
            //    character.AddComponent<BotMB>();

            //    NavMeshCharacter agent = character.GetComponent<NavMeshCharacter>();
            //    Bot bot = _botFactory.CreateBot(agent, _gamePoints);

            //    BotMB botMB = agent.gameObject.GetComponent<BotMB>();
            //    botMB.OnDie += _botController.DieRestartEntery; // переделать...


            //    _botController.AddBot(bot);
            //}


        }

        public void InitFinishData()
        {

        }


    }
}
