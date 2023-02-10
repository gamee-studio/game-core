using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gamee.Hiuk.Popup 
{
    public class PopupUpdate : UniPopup
    {
        private Action actionclose;

        public void Initialize(Action actionClose = null)
        {
            this.actionclose = actionClose;
        }

        public void Back() 
        {
            actionclose?.Invoke();
            Close();
        }
    }
}

