using Gamee.Hiuk.Popup;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamee.Hiuk.GameMenu.UI 
{
    public class GameMenuUI : MonoBehaviour
    {
        PopupManager popupManager;
        public Action ActionStartGame;
        public void Init() 
        {
            popupManager = PopupManager.Instance;
        }
        public void StartGame() 
        {
            ActionStartGame?.Invoke();
        }
        public void ShowPopupDebug() 
        {
            popupManager.ShowPopupDebug(null);
        }
        public void DefautUI() { }
        public void MoveUI() { }
    }
}

