using Assets._Scripts.Bots;
using Assets._Scripts.EnteryPoints.Interfaces;
using Assets._Scripts.GameControllers;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using VContainer.Unity;

namespace Assets._Scripts.EnteryPoints.Level
{
    public class BotEnteryPoint : IStartable, IDisposable, IInitFinish
    {
        private BotTransformList _botList;
        private BotsController _botController;
        public IEnumerable<IFinish> Finished { get; private set; }

        public BotEnteryPoint(BotsController botController, IEnumerable<IFinish> fineshed, BotTransformList botList)
        {
            _botController = botController;
            Finished = fineshed;
            _botList = botList;

            

        }

        public void Dispose()
        {

        }

        public void Start()
        {
            _botController.AddBotsTransformCollection(_botList);
        }

        public void InitFinishData()
        {

        }
    }
}
