using Assets._Scripts.GameControllers.Levels;
using System.Collections.Generic;
using VContainer.Unity;

namespace Assets.Scripts.EnteryPoints
{
    public class GameEnteryPoint : IStartable // не работает данный метод...
    {

        private readonly IEnumerable<ILevelInit> _levelInits;

        public GameEnteryPoint(IEnumerable<ILevelInit> levelInits) 
        {
            _levelInits = levelInits;
        }


        public void Start()
        {
            foreach (var levelInit in _levelInits)
            {
                levelInit.Initialize();
            }
        }
    }
}
