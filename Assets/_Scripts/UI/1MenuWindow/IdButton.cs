using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Assets._Scripts.UI._1MenuWindow
{
    public class IdButton : MonoBehaviour
    {
        [SerializeField] private int _id;

        public void OnButtonClick()
        {
            Debug.Log("Нажата кнопка: " + _id);
        }
    }
}
