using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.SaveLoad
{
    [CreateAssetMenu(fileName = "CheckpointsConfig", menuName = "Game/Checkpoints Config")]
    public class CheckpointsConfig : ScriptableObject
    {
        public List<CheckpointData> checkpoints = new List<CheckpointData>();

        [Serializable]
        public class CheckpointData
        {
            public string id;
            public string sceneName;
            public Vector3 position;
            public string displayName;
            public bool isActive = true;
        }

        public string GenerateNewID()
        {
            return $"CP_{System.Guid.NewGuid().ToString().Substring(0, 8)}";
        }
    }
}
