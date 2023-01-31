using Gamee.Hiuk.Ads;
using System;
using UnityEngine;

namespace Gamee.Hiuk.Popup 
{
    public class PopupLose : UniPopup
    {
        [SerializeField] GameObject btnReplay;
        [SerializeField] GameObject btnSkip;
        Action actionBackToHome;
        Action actionReplayLevel;
        Action actionSkipLevel;
        private bool isSellected = false;
        public void Initialize(Action actionBackToHome, Action actionReplayLevel, Action actionSkipLevel)
        {
            this.actionBackToHome = actionBackToHome;
            this.actionReplayLevel = actionReplayLevel;
            this.actionSkipLevel = actionSkipLevel;

            DefautUI();
        }

        public void BackToHome()
        {
            actionBackToHome?.Invoke();
            Close();
        }
        public void Skip()
        {
            if (isSellected) return;
            isSellected = true;

            AdsManager.ShowReard((isWatched) =>
            {
                if (isWatched)
                {
                    actionSkipLevel?.Invoke();
                    Close();
                }
            }, () => 
            {
                DefautUI();
            });
        }
        public void Replay() 
        {
            if (isSellected) return;
            isSellected = true;

            actionReplayLevel?.Invoke();
            Close();
        }
        void DefautUI() 
        {
            isSellected = false;
            btnReplay.gameObject.SetActive(true);
            btnSkip.gameObject.SetActive(true);
        }
    }
}

