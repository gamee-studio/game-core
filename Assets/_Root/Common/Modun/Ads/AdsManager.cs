using Gamee.Hiuk.Pattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Ads 
{
    public class AdsManager : Singleton<AdsManager>
    {
        [SerializeField] Admob admob;
        [SerializeField] Applovin applovin;
        [SerializeField] bool isHaveAppOpen;
        private IAds ads;
        private Action actionInterAdsClose;
        private Action actionRewardAdsOpen;
        private Action<bool> actionRewardAdsClose;

        public IAds ConfigAds(bool isAdmob = false) 
        {
            IAds ad = isAdmob ? admob : applovin;
            return ad;
        }
        public void SetAds(IAds ads) 
        {
            this.ads = ads;
        }

        public void InitAds()
        {
            ads.InitAds();
            HideBannerAds();

            ads.ActionCloseInterstitialAd = OnInterCloseAdsEvent;
            ads.ActionOnRewardAdDisplayed = OnRewardAdsOpenEvent;
            ads.ActionCloseRewardedAd = OnRewardCloseAdsEvent;
        }
        public void ShowBannerAds() 
        {
            ads.ShowBannerAds(); 
        }
        public void ShowInterAds(Action actionClose = null) 
        {
            this.actionInterAdsClose = actionClose;
            ads.ShowInterAds();
        }
        public void ShowReardAds(Action<bool> actionClose = null, Action actionOpen = null) 
        {
            this.actionRewardAdsClose = actionClose;
            this.actionRewardAdsOpen= actionOpen;

            ads.ShowRewardAds();
        }
        public void ShowAppOpenAds() 
        {
            ads.ShowAppOpenAds();
        }
        public void HideBannerAds() 
        {
            ads.HideBannerAds();
        }

        void OnInterCloseAdsEvent() 
        {
            actionInterAdsClose?.Invoke();
        }
        void OnRewardAdsOpenEvent() 
        {
            actionRewardAdsOpen?.Invoke();
        }
        void OnRewardCloseAdsEvent(bool isWatched)
        {
            actionRewardAdsClose?.Invoke(isWatched);
        }

        private void OnApplicationPause(bool pause)
        {
#if !UNITY_EDITOR
            if (!pause && isHaveAppOpen) { ads.ShowAppOpenAds(); }
#endif
        }

        #region satic api
        public static IAds Ads;
        public static void Config(bool isAdmob = false)
        {
            Ads = Instance.ConfigAds(isAdmob);
            Set(Ads);
        }
        public static void Set(IAds ads)
        {
            Instance.SetAds(ads);
        }

        public static void Init()
        {
            Instance.InitAds();
        }
        public static void ShowBanner()
        {
            Instance.ShowBannerAds();
        }
        public static void ShowInter(Action actionClose = null)
        {
            Instance.ShowInterAds(actionClose);
        }
        public static void ShowReard(Action<bool> actionClose = null, Action actionOpen = null)
        {
            Instance.ShowReardAds(actionClose, actionOpen);
        }
        public static void ShowAppOpen()
        {
            Instance.ShowAppOpenAds();
        }
        public static void HideBanner()
        {
            Instance.HideBannerAds();
        }
        #endregion
    }
}

