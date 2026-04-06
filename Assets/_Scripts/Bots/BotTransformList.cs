using UnityEngine;

namespace Assets._Scripts.Bots
{
    public class BotTransformList
    {
        public Transform Bots { get; private set; }

        public BotTransformList(Transform bots)
        {
            Bots = bots;
        }

    }
}
