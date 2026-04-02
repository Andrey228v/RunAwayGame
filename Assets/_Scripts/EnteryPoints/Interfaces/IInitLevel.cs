using Assets._Scripts.GameControllers;
using System.Collections.Generic;

namespace Assets._Scripts.EnteryPoints.Interfaces
{
    public interface IInitLevel
    {
        public IEnumerable<IInit> Inited { get; }

        public void InitLevelData();
    }
}
