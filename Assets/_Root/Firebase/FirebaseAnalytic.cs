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

        public const string AD_CLICK = "ad_click";
        public const string AD_BANNER_IMPRESSION = "ad_banner_impression";
        public const string AD_BANNER_REQUEST = "ad_banner_request";
        public const string AD_INTERSTITIAL_IMPRESSION = "ad_interstitial_impression";
        public const string AD_INTERSTITIAL_REQUEST = "ad_interstitial_request";
        public const string AD_REWARD_IMPRESSION = "ad_reward_impression";
        public const string AD_REWARD_REQUEST = "ad_reward_request";

        // paramater
        public const string LEVEL = "level";
        public const string SKIN = "skin";

        public static void LogEvent(string name, string paramater = null, string value = null)
        {
#if !UNITY_EDITOR
            if(paramater != null && value != null) 
            {
                Firebase.Analytics.FirebaseAnalytics.LogEvent(name, new Firebase.Analytics.Parameter(paramater, value));
            }else Firebase.Analytics.FirebaseAnalytics.LogEvent(name);
#endif
        }
    }
}

