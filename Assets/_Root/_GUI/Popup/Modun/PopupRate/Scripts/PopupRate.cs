using Gamee.Hiuk.AppLink;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gamee.Hiuk.Popup 
{
    public class PopupRate : UniPopup
    {
        private Action actionclose;

        public void Initialize(Action actionClose = null)
        {
            this.actionclose = actionClose;
        }

        public void Rate() 
        {
            ApplinkManager.Rate();
        }
        public void Back() 
        {
            actionclose?.Invoke();
            Close();
        }
    }
}

