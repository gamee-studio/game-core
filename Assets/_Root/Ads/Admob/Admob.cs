namespace Gamee.Hiuk.Ads
{
    using com.adjust.sdk;
    using Gamee.Hiuk.FirebseAnalytic;
    using GoogleMobileAds.Api;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Admob : MonoBehaviour, IAds
    {
        [SerializeField] List<string> listDeviceTest;
        [SerializeField] AdPosition bannerPos = AdPosition.Bottom;
        private BannerView bannerViewAd;
        private InterstitialAd interstitialAd;
        private RewardedAd rewardedAd;
        private AppOpenAd appOpenAd;

        private bool isInitialized = false;
        private bool isWatched = false;
        private bool isCanShowAppOpen = true;
        private bool isShowingAppOpenAd = false;
        private int interstitialRetryAttempt;
        private int rewardedRetryAttempt;
        public Action ActionCloseInterstitialAd { get; set; }
        public Action<bool> ActionCloseRewardedAd { get; set; }
        public Action ActionOnRewardAdDisplayed { get; set; }

        #region init
        public void InitAds()
        {
#if UNITY_IOS
        if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
        }
#endif
            isCanShowAppOpen = true;
            if (!isInitialized) 
            {
                MobileAds.Initialize(initStatus =>
                {
                    InitBannerAds();
                    InitInterAds();
                    InitRewardAds();
                    DeviceTest();
                    isInitialized = true;
                });
            }
        }
        private void DeviceTest()
        {
            RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(listDeviceTest)
            .build();

            MobileAds.SetRequestConfiguration(requestConfiguration);
        }
        public bool IsInitialized()
        {
            return isInitialized;
        }
        #endregion

        #region banner
        [SerializeField] string bannerAdUnitId;
        public void InitBannerAds()
        {
            this.bannerViewAd = new BannerView(bannerAdUnitId, AdSize.Banner, bannerPos);
            bannerViewAd.OnPaidEvent += (_, __) => HandleOnAdPaidCallback(_, __, bannerAdUnitId);
            LoadBannerAds();
        }
        public void LoadBannerAds()
        {
            FirebaseAnalytic.LogAdsBannerRequest();
            AdRequest request = new AdRequest.Builder().Build();
            this.bannerViewAd.LoadAd(request);
        }
        public void HideBannerAds()
        {
            if (this.bannerViewAd == null) return;
            this.bannerViewAd.Hide();
        }
        public void ShowBannerAds()
        {
            this.bannerViewAd.Show();
            FirebaseAnalytic.LogAdsBannerImpression();
        }

        // Handle
        #endregion

        #region inter 
        [SerializeField] string interAdUnitId;
        public void InitInterAds()
        {
            this.interstitialAd = new InterstitialAd(interAdUnitId);
            this.interstitialAd.OnAdLoaded += HandleOnInterAdLoaded;
            this.interstitialAd.OnAdFailedToLoad += HandleOnInterAdFailedToLoad;
            this.interstitialAd.OnAdClosed += HandleOnInterAdClose;
            interstitialAd.OnPaidEvent += (_, __) => HandleOnAdPaidCallback(_, __, interAdUnitId);

            LoadInterAds();
        }
        public bool IsInterReady()
        {
            if (this.interstitialAd == null) return false;
            return this.interstitialAd.IsLoaded();
        }
        public void LoadInterAds()
        {
            FirebaseAnalytic.LogAdsInterRequest();
            AdRequest request = new AdRequest.Builder().Build();
            this.interstitialAd.LoadAd(request);
        }
        public void ShowInterAds()
        {
            isCanShowAppOpen = false;
            this.interstitialAd.Show();
        }

        // Handle
        public void HandleOnInterAdLoaded(object sender, EventArgs args)
        {
            interstitialRetryAttempt = 0;
        }
        public void HandleOnInterAdFailedToLoad(object sender, EventArgs args)
        {
            interstitialRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetryAttempt));
            Debug.Log("Interstitial failed to load with error code: " + args);
            Invoke("LoadInterAds", (float)retryDelay);
        }
        public void HandleOnInterAdClose(object sender, EventArgs args)
        {
            ActionCloseInterstitialAd?.Invoke();
            this.LoadInterAds();
            FirebaseAnalytic.LogAdsInterImpression();
        }
        #endregion

        #region reward
        [SerializeField] string rewardAdUnitId;
        public void InitRewardAds()
        {
            this.rewardedAd = new RewardedAd(rewardAdUnitId);
            this.rewardedAd.OnAdLoaded += HandleOnRewardAdLoaded;
            this.rewardedAd.OnAdFailedToLoad += HandleOnRewardAdFailedToLoad;
            this.rewardedAd.OnUserEarnedReward += HandleOnUserEarnedReward;
            this.rewardedAd.OnAdClosed += HandleOnRewardedAdClose;
            this.rewardedAd.OnAdOpening += HandleOnRewardAdOpen;
            rewardedAd.OnPaidEvent += (_, __) => HandleOnAdPaidCallback(_, __, rewardAdUnitId);

            LoadRewardAds();
        }
        public bool IsRewardReady()
        {
            if (rewardedAd != null) return false;
            return this.rewardedAd.IsLoaded();
        }
        public void LoadRewardAds()
        {
            isWatched = false;
            FirebaseAnalytic.LogAdsRewardRequest();
            AdRequest request = new AdRequest.Builder().Build();
            this.rewardedAd.LoadAd(request);
        }
        public void ShowRewardAds()
        {
            isCanShowAppOpen = false;
            this.rewardedAd.Show();
        }

        // Handle
        public void HandleOnRewardAdLoaded(object sender, EventArgs args)
        {
            rewardedRetryAttempt = 0;
        }
        public void HandleOnRewardAdFailedToLoad(object sender, EventArgs args)
        {
            rewardedRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetryAttempt));

            Debug.Log("Rewarded ad failed to load with error code: " + args);
            Invoke("LoadRewardAds", (float)retryDelay);
        }
        public void HandleOnRewardedAdClose(object sender, EventArgs args)
        {
            StartCoroutine(DelayTime(0.15f, () =>
            {
                ActionCloseRewardedAd?.Invoke(isWatched);
                LoadRewardAds();
            }));
        }
        private void HandleOnRewardAdOpen(object sender, EventArgs e)
        {
            ActionOnRewardAdDisplayed?.Invoke();
        }
        public void HandleOnUserEarnedReward(object sender, Reward args)
        {
            isWatched = true;
            FirebaseAnalytic.LogAdsRewardImpression();
        }

        IEnumerator DelayTime(float time = 0.5f, Action actionCompleted = null)
        {
            yield return new WaitForSeconds(time);
            actionCompleted?.Invoke();
        }
        #endregion

        #region app open
        [SerializeField] string appOpenAdUnitId;
        public bool IsAppOpenReady()
        {
            return this.appOpenAd != null;
        }

        public void InitAppOpenAds()
        {
            this.appOpenAd = null;
            LoadAppOpenAds();

            this.appOpenAd.OnAdDidDismissFullScreenContent += HandleOnAdDidDismissFullScreenContent;
            this.appOpenAd.OnAdFailedToPresentFullScreenContent += HandleOnAdFailedToPresentFullScreenContent;
            this.appOpenAd.OnAdDidPresentFullScreenContent += HandleOnAdDidPresentFullScreenContent;
            this.appOpenAd.OnAdDidRecordImpression += HandleOnAdDidRecordImpression;
            this.appOpenAd.OnPaidEvent += (_, __) => HandleOnAdPaidCallback(_, __, rewardAdUnitId);
        }

        public void LoadAppOpenAds()
        {
            var request = new AdRequest.Builder().Build();

            // Load an app open ad for portrait orientation
            AppOpenAd.LoadAd(appOpenAdUnitId, ScreenOrientation.Portrait, request, ((appOpenAd, error) =>
            {
                if (error != null)
                {
                    // Handle the error.
                    Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
                    return;
                }

                // App open ad is loaded
                this.appOpenAd = appOpenAd;
                Debug.Log("App open ad loaded");
            }));
        }

        public void ShowAppOpenAds()
        {
            if (!isCanShowAppOpen)
            {
                isCanShowAppOpen = true;
                return;
            }
            if (isShowingAppOpenAd) return;
            this.appOpenAd.Show();
        }

        // Handle
        private void HandleOnAdDidDismissFullScreenContent(object sender, EventArgs args)
        {
            Debug.Log("Closed app open ad");
            // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
            isShowingAppOpenAd = false;
            InitAppOpenAds();
        }

        private void HandleOnAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args)
        {
            Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
            // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
            InitAppOpenAds();
        }

        private void HandleOnAdDidPresentFullScreenContent(object sender, EventArgs args)
        {
            Debug.Log("Displayed app open ad");
            isShowingAppOpenAd = true;
        }

        private void HandleOnAdDidRecordImpression(object sender, EventArgs args)
        {
            Debug.Log("Recorded ad impression");
        }
        #endregion

        //  log payment
        private void HandleOnAdPaidCallback(object sender, AdValueEventArgs e, string id)
        {
            var adValue = e.AdValue;

            // Log an event with ad value parameters
            Firebase.Analytics.Parameter[] LTVParameters =
            {
               // Log ad value in micros.
               new Firebase.Analytics.Parameter("valuemicros", adValue.Value),
               // These values below won’t be used in ROASrecipe.
               // But log for purposes of debugging and futurereference.
               new Firebase.Analytics.Parameter("currency", adValue.CurrencyCode), new Firebase.Analytics.Parameter("precision", (int) adValue.Precision),
               new Firebase.Analytics.Parameter("adunitid", id), new Firebase.Analytics.Parameter("network", "admob")
               };
            Firebase.Analytics.FirebaseAnalytics.LogEvent("paid_ad_impression", LTVParameters);

            AdjustAdRevenue adRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAdMob);
            adRevenue.setRevenue(adValue.Value / 1000000f, adValue.CurrencyCode);
            Adjust.trackAdRevenue(adRevenue);
        }
    }
}
