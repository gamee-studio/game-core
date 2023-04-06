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

        public const string DAILY_CLAIM_DAY_1 = "daily_claim_day_1";
        public const string DAILY_CLAIM_DAY_3 = "daily_claim_day_3";
        public const string DAILY_CLAIM_DAY_7 = "daily_claim_day_7";
        public const string DAILY_CLAIM_BONUS = "daily_claim_bonus";

        public const string FIRST_SKIN_HERO_UNLOCK = "first_skin_hero_unlock";
        public const string FIRST_SKIN_PRINCESS_UNLOCK = "first_skin_princess_unlock";
        public const string GIFT_CODE_UNLOCK = "gift_code_unlock";
        public const string GOLD_PIN_UNLOCK = "gold_pin_unlock";
        public const string ADS_PIN_UNLOCK = "ads_pin_unlock";
        public const string GIFT_PIN_UNLOCK = "gift_pin_unlock";
        public const string ROOM_UNLOCK = "room_{0}_unlock";
        public const string ROOM_COMPLETED = "room_{0}_completed";

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
        #region log daily
        public static void LogDailyClaimDay1()
        {
            LogEvent(DAILY_CLAIM_DAY_1);
        }
        public static void LogDailyClaimDay3()
        {
            LogEvent(DAILY_CLAIM_DAY_3);
        }
        public static void LogDailyClaimDay7()
        {
            LogEvent(DAILY_CLAIM_DAY_7);
        }
        public static void LogDailyClaimBonus()
        {
            LogEvent(DAILY_CLAIM_BONUS);
        }
        #endregion
        #region log skin
        public static void LogFirstSkinHeroUnlock(string value)
        {
            LogEvent(FIRST_SKIN_HERO_UNLOCK, LEVEL, value);
        }
        public static void LogFirstSkinPrincessUnlock(string value)
        {
            LogEvent(FIRST_SKIN_PRINCESS_UNLOCK, LEVEL, value);
        }
        public static void LogGiftCodeUnlock()
        {
            LogEvent(GIFT_CODE_UNLOCK);
        }
        public static void LogGoldPinUnlock()
        {
            LogEvent(GOLD_PIN_UNLOCK);
        }
        public static void LogAdsPinUnlock()
        {
            LogEvent(ADS_PIN_UNLOCK);
        }
        public static void LogGiftPinUnlock()
        {
            LogEvent(GIFT_PIN_UNLOCK);
        }
        #endregion
        #region log decor
        public static void LogRoomUnlock(int index, int level)
        {
            LogEvent(string.Format(ROOM_UNLOCK, index), LEVEL, level.ToString());
        }
        public static void LogRoomCompleted(int index, int level)
        {
            LogEvent(string.Format(ROOM_COMPLETED, index), LEVEL, level.ToString());
        }
        #endregion
    }
}

