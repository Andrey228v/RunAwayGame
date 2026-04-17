using Assets._Scripts.Bots;
using Assets._Scripts.GameControllers;
using Assets.Scripts.Points;
using Assets.Scripts.SaveLoad;
using ECM2;
using System;
using System.Collections.Generic;

namespace Assets._Scripts.EnteryPoints.Level
{
    public class BotAIEnteryPoint // Нужен ли ....
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
    }
}
