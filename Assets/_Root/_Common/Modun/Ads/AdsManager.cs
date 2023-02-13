using Gamee.Hiuk.Pattern;
using System;
using UnityEngine;

namespace Gamee.Hiuk.Ads
{
    public class AdsManager : Singleton<AdsManager>
    {
        [SerializeField] Admob admob;
        [SerializeField] Applovin applovin;
        [SerializeField] bool isHaveAppOpen = true;
        [SerializeField] bool isShowAppOpenInFirstTime = false;

        private IAds ads;
        private Action actionInterAdsClose;
        private Action actionRewardAdsOpen;
        private Action<bool> actionRewardAdsClose;
        private bool isRemoveRewardAds;
        private bool isRemoveInterAds;
        private bool isRemoveBannerAds;
        private bool isRemoveAppOpenAds;
        private bool isShowAppOpen;
        private bool isFirstShowAppOpenCache;

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
            isShowAppOpen = true;
            ads.InitAds();
            HideBannerAds();

            ads.ActionCloseInterstitialAd = OnInterCloseAdsEvent;
            ads.ActionOnRewardAdDisplayed = OnRewardAdsOpenEvent;
            ads.ActionCloseRewardedAd = OnRewardCloseAdsEvent;
        }
        public void ShowBannerAds()
        {
            if (isRemoveBannerAds) return;
            ads.ShowBannerAds();
        }
        public void ShowInterAds(Action actionClose = null)
        {
            if (isRemoveInterAds)
            {
                actionClose?.Invoke();
            }
            else
            {
                this.actionInterAdsClose = actionClose;
                ads.ShowInterAds();
            }
        }
        public void ShowReardAds(Action<bool> actionClose = null, Action actionOpen = null)
        {
            if (isRemoveRewardAds)
            {
                actionOpen?.Invoke();
                actionClose?.Invoke(true);
            }
            else
            {
                this.actionRewardAdsClose = actionClose;
                this.actionRewardAdsOpen = actionOpen;

                ads.ShowRewardAds();
            }
        }
        public void ShowAppOpenAds()
        {
            if (isRemoveAppOpenAds || !isHaveAppOpen) return;
            if (!isShowAppOpenInFirstTime)
            {
                if (!isFirstShowAppOpenCache)
                {
                    isFirstShowAppOpenCache = true;
                    return;
                }
            }

            if (!isShowAppOpen) return;
            ads.ShowAppOpenAds();
        }
        public void SetShowAppOpenAds(bool isShow) { isShowAppOpen = isShow; }
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

        public void SetRemoveRewardAds(bool isRemove) { isRemoveRewardAds = isRemove; }
        public void SetRemoveInterAds(bool isRemove) { isRemoveInterAds = isRemove; }
        public void SetRemoveAppOpenAds(bool isRemove) { isRemoveAppOpenAds = isRemove; }
        public void SetRemoveBannerAds(bool isRemove) { isRemoveBannerAds = isRemove; }
        private void OnApplicationPause(bool pause)
        {
#if !UNITY_EDITOR
            if (!pause) { ads.ShowAppOpenAds(); }
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
        public static bool IsInterAdsReady => Instance.ads.IsInterReady();
        public static bool IsRewardAdsReady => Instance.ads.IsRewardReady();
        public static void SetRemoveReward(bool isRemove) { Instance.SetRemoveRewardAds(isRemove); }
        public static void SetRemoveInter(bool isRemove) { Instance.SetRemoveInterAds(isRemove); }
        public static void SetRemoveAppOpen(bool isRemove) { Instance.SetRemoveAppOpenAds(isRemove); }
        public static void SetRemoveBanner(bool isRemove) { Instance.SetRemoveBannerAds(isRemove); }
        public static void SetShowAppOpen(bool isShow) { Instance.SetShowAppOpenAds(isShow); }
        #endregion
    }
}