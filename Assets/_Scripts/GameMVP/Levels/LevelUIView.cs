using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets._Scripts.GameMVP.Levels
{
    public class LevelUIView : MonoBehaviour
    {
        [SerializeField] private string _id;

        public string Id => _id;
    }
}
