using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets._Scripts.Loger
{
    public interface IGameLogger
    {
        void Log(string message, string category = "Info");
        void LogWarning(string message, string category = "Warning");
        void LogError(string message, string category = "Error");
        void WithTag(string tag); // Для смены тега
    }

    public class UnityLogger : IGameLogger
    {
        private string _currentTag;
        private Dictionary<string, string> _categoryColors;

        public UnityLogger(string defaultTag = "Game")
        {
            _currentTag = defaultTag;

            _categoryColors = new Dictionary<string, string>
            {
                ["Info"] = "white",
                ["Warning"] = "yellow",
                ["Error"] = "red",
                ["Success"] = "green",
                ["Debug"] = "grey",
                ["Network"] = "cyan",
                ["Save"] = "lime",
                ["Load"] = "blue",
                ["Event"] = "magenta",
                ["Level"] = "orange",
                ["Achievment"] = "black"
            };
        }

        public void WithTag(string tag) => _currentTag = tag;

        public void Log(string message, string category = "Info")
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            string color = GetColorForCategory(category);
            Debug.Log($"<color={color}>[{category}]</color> [<color=cyan>{_currentTag}</color>] {message}");
#endif
        }

        public void LogWarning(string message, string category = "Warning")
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogWarning($"<color=yellow>[{category}]</color> [<color=cyan>{_currentTag}</color>] {message}");
#endif
        }

        public void LogError(string message, string category = "Error")
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.LogError($"<color=red>[{category}]</color> [<color=cyan>{_currentTag}</color>] {message}");
#endif
        }

        public void LogSuccess(string message, string category = "Success")
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.Log($"<color=green>[{category}]</color> [<color=cyan>{_currentTag}</color>] {message}");
#endif
        }

        public void LogCustom(string message, string category, string color)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            Debug.Log($"<color={color}>[{category}]</color> [<color=cyan>{_currentTag}</color>] {message}");
#endif
        }

        private string GetColorForCategory(string category)
        {
            return _categoryColors.ContainsKey(category) ? _categoryColors[category] : "white";
        }
    }
}
