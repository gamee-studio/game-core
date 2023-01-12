using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Ads 
{
    public interface IAds
    {
        void InitAds();
        bool IsInitialized();

        void InitBannerAds();
        void LoadBannerAds();
        void ShowBannerAds();
        void HideBannerAds();

        bool IsInterReady();
        void InitInterAds();
        void LoadInterAds();
        void ShowInterAds();

        bool IsRewardReady();
        void InitRewardAds();
        void LoadRewardAds();
        void ShowRewardAds();

        bool IsAppOpenReady();
        void InitAppOpenAds();
        void LoadAppOpenAds();
        void ShowAppOpenAds();

        Action<bool> ActionCloseRewardedAd { set; get; }
        Action ActionOnRewardAdDisplayed { set; get; }
        Action ActionCloseInterstitialAd { set; get; }
    }
}

