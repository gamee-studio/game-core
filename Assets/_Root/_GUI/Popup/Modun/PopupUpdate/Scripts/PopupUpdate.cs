using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gamee.Hiuk.Popup 
{
    public class PopupUpdate : UniPopup
    {
        [SerializeField] TextMeshProUGUI txtDescription;
        [SerializeField] TextMeshProUGUI txtVersion;
        [SerializeField] Toggle toggleNotShowAgain;
        private Action<bool> actionclose;

        public void Initialize(Action<bool> actionClose, string strDescription, string strVersionUpdate)
        {
            this.actionclose = actionClose;

            txtDescription.text = strDescription.Replace("\\n", "\n");
            txtVersion.text = "Version: " + strVersionUpdate;
        }

        public void Back() 
        {
            actionclose?.Invoke(toggleNotShowAgain.isOn);
            Close();
        }
    }
}

