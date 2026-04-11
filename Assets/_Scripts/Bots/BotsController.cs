using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace Assets._Scripts.Bots
{
    //Класс, который хранит в себе всех ботом и запускает их StateMachine. Контролирует их количество, и создаёт/уничтожает.
    public class BotsController : IFixedTickable, IStartable, IDisposable
    {
        private List<Bot> _bots = new List<Bot>();
        private BotFactory _botFactory;

        public BotsController(BotFactory botFactory)
        {
            _botFactory = botFactory;
        }

        public void Dispose()
        {
            foreach (Bot bot in _bots)
            {
                bot.Dispose();
            }

            _bots.Clear();
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

            //Тут мы должны передать точки боту по которым он побежит.
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
    }
}
