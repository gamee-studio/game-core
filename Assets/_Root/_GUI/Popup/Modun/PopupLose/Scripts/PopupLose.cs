using Gamee.Hiuk.Ads;
using System;

namespace Gamee.Hiuk.Popup 
{
    public class PopupLose : UniPopup
    {
        Action actionBackToHome;
        Action actionReplayLevel;
        Action actionSkipLevel;
        public void Initialize(Action actionBackToHome, Action actionReplayLevel, Action actionSkipLevel)
        {
            this.actionBackToHome = actionBackToHome;
            this.actionReplayLevel = actionReplayLevel;
            this.actionSkipLevel = actionSkipLevel;
        }

        public void BackToHome()
        {
            actionBackToHome?.Invoke();
        }
        public void Skip()
        {
            AdsManager.ShowReard((isWatched) =>
            {
                if (isWatched)
                {
                    actionSkipLevel?.Invoke();
                }
            });
        }
        public void Replay() 
        {
            actionReplayLevel?.Invoke();
        }
    }
}

