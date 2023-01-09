namespace Gamee.Hiuk.Ads
{
    using com.adjust.sdk;
    using Firebase.Analytics;
    using Gamee.Hiuk.FirebseAnalytic;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class Applovin : MonoBehaviour, IAds
    {
        [SerializeField] MaxSdkBase.BannerPosition bannerPos = MaxSdkBase.BannerPosition.BottomCenter;
        private int interstitialRetryAttempt;
        private int rewardedRetryAttempt;
        private bool isCanShowAppOpen;
        private bool isWatched = false;

        public Action ActionCloseInterstitialAd { get; set; }
        public Action<bool> ActionCloseRewardedAd { get; set; }
        public Action ActionOnRewardAdDisplayed { get; set; }

        #region init
        [SerializeField] string sdkKey = "-feJa9bEGOmZW95XxkyfhE2R_yHQ4poWZofsvPWhIw_es2dT16vUIDRoHKX63m6a9JD7wX1Q0PZ0Qng8EukpFT";
        public void InitAds()
        {
            isCanShowAppOpen = true;
            MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
            {
                if (!IsInitialized())
                {
                    Debug.Log("[Max] init completed!");
                    // AppLovin SDK is initialized, start loading ads
                    InitBannerAds();
                    InitInterAds();
                    InitRewardAds();
                    InitAppOpenAds();
                }
            };

            MaxSdk.SetSdkKey(sdkKey);
            MaxSdk.InitializeSdk();
        }
        public bool IsInitialized()
        {
            return MaxSdk.IsInitialized();
        }
        #endregion

        #region banner
        [SerializeField] string bannerAdUnitId;
        public void InitBannerAds()
        {
            LoadBannerAds();
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
            Debug.Log("[Max] banner init!");
        }
        public void LoadBannerAds()
        {
            FirebaseAnalytic.LogAdsBannerRequest();
            // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
            // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
            MaxSdk.CreateBanner(bannerAdUnitId, bannerPos);

            // Set background or background color for banners to be fully functional
            MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, Color.black);
        }
        public void HideBannerAds()
        {
            MaxSdk.HideBanner(bannerAdUnitId);
        }
        public void ShowBannerAds()
        {
            Debug.Log("[Max] banner show!");
            MaxSdk.ShowBanner(bannerAdUnitId);
            FirebaseAnalytic.LogAdsBannerImpression();
        }
        #endregion

        #region inter 
        [SerializeField] string interAdUnitId;
        public void InitInterAds()
        {
            // Attach callbacks
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += InterstitialFailedToDisplayEvent;
            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;

            // Load the first interstitial
            LoadInterAds();
        }
        public bool IsInterReady()
        {
            return MaxSdk.IsInterstitialReady(interAdUnitId);
        }
        public void LoadInterAds()
        {
            FirebaseAnalytic.LogAdsInterRequest();
            MaxSdk.LoadInterstitial(interAdUnitId);
        }
        public void ShowInterAds()
        {
            if (IsInterReady())
            {
                isCanShowAppOpen = false;
                MaxSdk.ShowInterstitial(interAdUnitId);
                FirebaseAnalytic.LogAdsInterImpression();
            }
        }

        // Handle
        private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(interstitialAdUnitId) will now return 'true'
            Debug.Log("Interstitial loaded");

            // Reset retry attempt
            interstitialRetryAttempt = 0;
        }

        private void OnInterstitialFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            // Interstitial ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
            interstitialRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, interstitialRetryAttempt));
            Debug.Log("Interstitial failed to load with error code: " + errorInfo.Code);
            Invoke("LoadInterAds", (float)retryDelay);
        }

        private void InterstitialFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad failed to display. We recommend loading the next ad
            Debug.Log("Interstitial failed to display with error code: " + errorInfo.Code);
            LoadInterAds();
        }

        private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Interstitial ad is hidden. Pre-load the next ad.
            ActionCloseInterstitialAd?.Invoke();
            LoadInterAds();
        }
        #endregion

        #region reward
        [SerializeField] string rewardAdUnitId;
        public void InitRewardAds()
        {
            // Attach callbacks
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdFailedEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
            MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;

            // Load the first RewardedAd
            LoadRewardAds();
        }
        public bool IsRewardReady()
        {
            return MaxSdk.IsRewardedAdReady(rewardAdUnitId);
        }
        public void LoadRewardAds()
        {
            FirebaseAnalytic.LogAdsRewardRequest();
            isWatched = false;
            MaxSdk.LoadRewardedAd(rewardAdUnitId);
        }
        public void ShowRewardAds()
        {
            if (IsRewardReady())
            {
                isCanShowAppOpen = false;
                MaxSdk.ShowRewardedAd(rewardAdUnitId);
            }
        }

        // Handle
        private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(rewardedAdUnitId) will now return 'true'
            Debug.Log("Rewarded ad loaded");

            // Reset retry attempt
            rewardedRetryAttempt = 0;
        }

        private void OnRewardedAdFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
        {
            // Rewarded ad failed to load. We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds).
            rewardedRetryAttempt++;
            double retryDelay = Math.Pow(2, Math.Min(6, rewardedRetryAttempt));

            Debug.Log("Rewarded ad failed to load with error code: " + errorInfo.Code);
            Invoke("LoadRewardAds", (float)retryDelay);
        }

        private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad failed to display. We recommend loading the next ad
            Debug.Log("Rewarded ad failed to display with error code: " + errorInfo.Code);
            LoadRewardAds();
        }

        private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            ActionOnRewardAdDisplayed?.Invoke();
            Debug.Log("Rewarded ad displayed");
        }

        private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            Debug.Log("Rewarded ad clicked");
        }

        private void OnRewardedAdDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad is hidden. Pre-load the next ad
            Debug.Log("Rewarded ad dismissed");
            LoadRewardAds();
        }
        private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad is hidden. Pre-load the next ad
            StartCoroutine(DelayTime(0.15f, () =>
            {
                ActionCloseRewardedAd?.Invoke(isWatched);
                LoadRewardAds();
            }));
        }

        private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
        {
            // Rewarded ad was displayed and user should receive the reward
            Debug.Log("Rewarded ad received reward");
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
            return MaxSdk.IsAppOpenAdReady(appOpenAdUnitId);
        }

        public void InitAppOpenAds()
        {
            MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += OnAppOpenDismissedEvent;
            MaxSdkCallbacks.AppOpen.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        }

        public void LoadAppOpenAds()
        {
            MaxSdk.LoadAppOpenAd(appOpenAdUnitId);
        }

        public void ShowAppOpenAds()
        {
            if (MaxSdk.IsAppOpenAdReady(appOpenAdUnitId))
            {
                if (!isCanShowAppOpen)
                {
                    isCanShowAppOpen = true;
                    return;
                }
                MaxSdk.ShowAppOpenAd(appOpenAdUnitId);
            }
            else
            {
                MaxSdk.LoadAppOpenAd(appOpenAdUnitId);
            }
        }

        // Handle
        public void OnAppOpenDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            LoadAppOpenAds();
        }
        #endregion

        #region log payment
        private void OnAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
        {
            // Log an event with ad value parameters
            double revenue = adInfo.Revenue;

            Debug.Log("adinfo.Revenue: " + adInfo.Revenue);
            // Ad revenue paid. Use this callback to track user revenue.
            // send ad revenue info to Adjust
            AdjustAdRevenue adRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAppLovinMAX);
            adRevenue.setRevenue(adInfo.Revenue, "USD");
            adRevenue.setAdRevenueNetwork(adInfo.NetworkName);
            adRevenue.setAdRevenuePlacement(adInfo.Placement);
            adRevenue.setAdRevenueUnit(adInfo.AdUnitIdentifier);
            Adjust.trackAdRevenue(adRevenue);

            // Log an event with ad value parameters
            Parameter[] LTVParameters =
            {
                // Log ad value in micros.
                new Parameter("value", revenue),
                new Parameter("ad_platform", "AppLovin"),
                new Parameter("ad_format", adInfo.AdFormat),
                new Parameter("currency", "USD"),
                new Parameter("ad_unit_name", adInfo.AdUnitIdentifier),
                new Parameter("ad_source", adInfo.NetworkName)
            };

            FirebaseAnalytics.LogEvent("ad_impression", LTVParameters);
        }
        #endregion
    }
}

