using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.FirebseAnalytic
{
    public static class FirebaseAnalytic
    {
        // name
        public const string LEVEL_COMPLETE = "level_complete";
        public const string LEVEL_FAILED = "level_failed";
        public const string LEVEL_SKIP = "level_skip";
        public const string LEVEL_REPLAY = "level_replay";
        public const string LEVEL_START = "level_start";
        public const string LEVEL_FIRST_START = "level_first_start";

        public const string ADS_BANNER_IMPRESSION = "ad_banner_impression";
        public const string ADS_BANNER_REQUEST = "ad_banner_request";
        public const string ADS_INTERSTITIAL_IMPRESSION = "ad_interstitial_impression";
        public const string ADS_INTERSTITIAL_REQUEST = "ad_interstitial_request";
        public const string ADS_REWARD_IMPRESSION = "ad_reward_impression";
        public const string ADS_REWARD_REQUEST = "ad_reward_request";

        // paramater
        public const string LEVEL = "level";

        public static void LogEvent(string name, string paramater = null, string value = null)
        {
#if !UNITY_EDITOR
            if(paramater != null && value != null) 
            {
                Firebase.Analytics.FirebaseAnalytics.LogEvent(name, new Firebase.Analytics.Parameter(paramater, value));
            }else Firebase.Analytics.FirebaseAnalytics.LogEvent(name);
#endif
        }

        #region log level
        public static void LogLevelCompleted(string value) 
        {
            LogEvent(LEVEL_COMPLETE, LEVEL, value);
        }
        public static void LogLevelFailed(string value)
        {
            LogEvent(LEVEL_FAILED, LEVEL, value);
        }
        public static void LogLevelSkip(string value)
        {
            LogEvent(LEVEL_SKIP, LEVEL, value);
        }
        public static void LogLevelReplay(string value)
        {
            LogEvent(LEVEL_REPLAY, LEVEL, value);
        }
        public static void LogLevelStart(string value)
        {
            LogEvent(LEVEL_START, LEVEL, value);
        }
        public static void LogLevelFirstStart(string value)
        {
            LogEvent(LEVEL_FIRST_START, LEVEL, value);
        }
        #endregion

        #region log ads
        public static void LogAdsBannerImpression()
        {
            LogEvent(ADS_BANNER_IMPRESSION);
        }
        public static void LogAdsBannerRequest()
        {
            LogEvent(ADS_BANNER_REQUEST);
        }
        public static void LogAdsInterImpression()
        {
            LogEvent(ADS_INTERSTITIAL_IMPRESSION);
        }
        public static void LogAdsInterRequest()
        {
            LogEvent(ADS_INTERSTITIAL_REQUEST);
        }
        public static void LogAdsRewardImpression()
        {
            LogEvent(ADS_REWARD_IMPRESSION);
        }
        public static void LogAdsRewardRequest()
        {
            LogEvent(ADS_REWARD_REQUEST);
        }
        #endregion
    }
}

