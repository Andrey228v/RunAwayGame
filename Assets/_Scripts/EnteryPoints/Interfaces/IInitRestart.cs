using Assets._Scripts.GameControllers;
using System.Collections.Generic;

namespace Assets._Scripts.EnteryPoints.Interfaces
{
    public interface IInitRestart
    {
        public IEnumerable<IRestart> Restarted { get; }

        public void InitRestartData();
    }
}
