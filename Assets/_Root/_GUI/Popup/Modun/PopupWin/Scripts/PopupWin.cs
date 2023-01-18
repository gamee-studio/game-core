using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gamee.Hiuk.Popup 
{
    public class PopupWin : UniPopup
    {
        private Action actionBackToHome;
        private Action actionNextLevel;
        private Action actionWatchVideo;
        private Action actionProcessFull;

        public void Initialize(Action actionBackToHome, Action actionNextLevel, Action actionWatchVideo, Action actionProcessFull = null)
        {
            this.actionBackToHome = actionBackToHome;
            this.actionNextLevel = actionNextLevel;
            this.actionWatchVideo = actionWatchVideo;
            this.actionProcessFull = actionProcessFull;
        }

        public void NextLevel() 
        {
            actionNextLevel?.Invoke();
        }
        public void WatchVideo() 
        {
            actionWatchVideo?.Invoke();
        }
        public void BackToHome() 
        {
            actionBackToHome?.Invoke();
        }
        void AddCoin(int coin)
        {
            
        }
    }
}

