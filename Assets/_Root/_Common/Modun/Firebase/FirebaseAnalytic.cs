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

        public const string LEVEL_CHALLENGE_COMPLETE = "level_complete";
        public const string LEVEL_CHALLENGE_FAILED = "level_failed";
        public const string LEVEL_CHALLENGE_SKIP = "level_skip";
        public const string LEVEL_CHALLENGE_REPLAY = "level_replay";
        public const string LEVEL_CHALLENGE_START = "level_start";
        public const string LEVEL_CHALLENGE_FIRST_START = "level_first_start";

        public const string LEVEL_DRAW_COMPLETE = "level_complete";
        public const string LEVEL_DRAW_FAILED = "level_failed";
        public const string LEVEL_DRAW_SKIP = "level_skip";
        public const string LEVEL_DRAW_REPLAY = "level_replay";
        public const string LEVEL_DRAW_START = "level_start";
        public const string LEVEL_DRAW_FIRST_START = "level_first_start";

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

        public const string DAILY_MISSION_CLAIM = "daily_mission_claim";
        public const string DAILY_MISSION_SKIP = "daily_mission_skip";
        public const string DAILY_MISSION_CHEST_CLAIM = "daily_mission_chest_claim";

        public const string FIRST_PLAY_CHALLENGE_MODE = "first_play_challenge_mode";
        public const string SECOND_PLAY_CHALLENGE_MODE = "second_play_challenge_mode";
        public const string SHOW_UNLOCK_CHALLENGE_MODE = "challenge_mode_show_unlock";
        public const string CLICK_CHANGE_CHALLENGE_MODE = "challenge_mode_click_change";

        public const string FIRST_PLAY_DRAW_MODE = "first_play_draw_mode";
        public const string SECOND_PLAY_DRAW_MODE = "second_play_draw_mode";
        public const string SHOW_UNLOCK_DRAW_MODE = "draw_mode_show_unlock";
        public const string CLICK_CHANGE_DRAW_MODE = "draw_mode_click_change";

        public const string SKIN_ADS_UNLOCK = "skin_ads_unlock";

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
            LogEvent(LEVEL_COMPLETE, LEVEL_COMPLETE, value);
        }
        public static void LogLevelFailed(string value)
        {
            LogEvent(LEVEL_FAILED, LEVEL_FAILED, value);
        }
        public static void LogLevelSkip(string value)
        {
            LogEvent(LEVEL_SKIP, LEVEL_SKIP, value);
        }
        public static void LogLevelReplay(string value)
        {
            LogEvent(LEVEL_REPLAY, LEVEL_REPLAY, value);
        }
        public static void LogLevelStart(string value)
        {
            LogEvent(LEVEL_START, LEVEL_START, value);
        }
        public static void LogLevelFirstStart(string value)
        {
            LogEvent(LEVEL_FIRST_START, LEVEL_FIRST_START, value);
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
            LogEvent(FIRST_SKIN_HERO_UNLOCK, FIRST_SKIN_HERO_UNLOCK, value);
        }
        public static void LogFirstSkinPrincessUnlock(string value)
        {
            LogEvent(FIRST_SKIN_PRINCESS_UNLOCK, FIRST_SKIN_PRINCESS_UNLOCK, value);
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
            LogEvent(string.Format(ROOM_UNLOCK, index), string.Format(ROOM_UNLOCK, index), $"level_{level}");
        }
        public static void LogRoomCompleted(int index, int level)
        {
            LogEvent(string.Format(ROOM_COMPLETED, index), string.Format(ROOM_COMPLETED, index), $"level_{level}");
        }
        #endregion
        #region log daily mission
        public static void LogDailyMissionClaim()
        {
            LogEvent(DAILY_MISSION_CLAIM);
        }
        public static void LogDailyMissionSkip()
        {
            LogEvent(DAILY_MISSION_SKIP);
        }
        public static void LogDailyMissionChestClaim()
        {
            LogEvent(DAILY_MISSION_CHEST_CLAIM);
        }
        public static void LogUnlockSkinAds()
        {
            LogEvent(SKIN_ADS_UNLOCK);
        }
        #endregion
        #region challenge
        public static void LogLevelChallengeCompleted(string value)
        {
            LogEvent(LEVEL_CHALLENGE_COMPLETE, LEVEL_CHALLENGE_COMPLETE, value);
        }
        public static void LogLevelChallengeFailed(string value)
        {
            LogEvent(LEVEL_CHALLENGE_FAILED, LEVEL_CHALLENGE_FAILED, value);
        }
        public static void LogLevelChallengeSkip(string value)
        {
            LogEvent(LEVEL_CHALLENGE_SKIP, LEVEL_CHALLENGE_SKIP, value);
        }
        public static void LogLevelChallengeReplay(string value)
        {
            LogEvent(LEVEL_CHALLENGE_REPLAY, LEVEL_CHALLENGE_REPLAY, value);
        }
        public static void LogLevelChallengeStart(string value)
        {
            LogEvent(LEVEL_CHALLENGE_START, LEVEL_CHALLENGE_START, value);
        }
        public static void LogLevelChallengeFirstStart(string value)
        {
            LogEvent(LEVEL_CHALLENGE_FIRST_START, LEVEL_CHALLENGE_FIRST_START, value);
        }
        public static void LogFirstPlayChallengeMode(string value)
        {
            LogEvent(FIRST_PLAY_CHALLENGE_MODE, FIRST_PLAY_CHALLENGE_MODE, value);
        }     
        public static void LogSecondPlayChallengeMode(string value)
        {
            LogEvent(SECOND_PLAY_CHALLENGE_MODE, SECOND_PLAY_CHALLENGE_MODE, value);
        }
        public static void LogShowUnlockChallengeMode()
        {
            LogEvent(SHOW_UNLOCK_CHALLENGE_MODE);
        }
        public static void LogClickChangeChallengeMode()
        {
            LogEvent(CLICK_CHANGE_CHALLENGE_MODE);
        }
        #endregion
        #region draw
        public static void LogLevelDrawCompleted(string value)
        {
            LogEvent(LEVEL_DRAW_COMPLETE, LEVEL_DRAW_COMPLETE, value);
        }
        public static void LogLevelDrawFailed(string value)
        {
            LogEvent(LEVEL_DRAW_FAILED, LEVEL_DRAW_FAILED, value);
        }
        public static void LogLevelDrawSkip(string value)
        {
            LogEvent(LEVEL_DRAW_SKIP, LEVEL_DRAW_SKIP, value);
        }
        public static void LogLevelDrawReplay(string value)
        {
            LogEvent(LEVEL_DRAW_REPLAY, LEVEL_DRAW_REPLAY, value);
        }
        public static void LogLevelDrawStart(string value)
        {
            LogEvent(LEVEL_DRAW_START, LEVEL_DRAW_START, value);
        }
        public static void LogLevelDrawFirstStart(string value)
        {
            LogEvent(LEVEL_DRAW_FIRST_START, LEVEL_DRAW_FIRST_START, value);
        }
        public static void LogFirstPlayDrawMode(string value)
        {
            LogEvent(FIRST_PLAY_DRAW_MODE, FIRST_PLAY_DRAW_MODE, value);
        }
        public static void LogSecondPlayDrawMode(string value)
        {
            LogEvent(SECOND_PLAY_DRAW_MODE, SECOND_PLAY_DRAW_MODE, value);
        }
        public static void LogShowUnlockDrawMode()
        {
            LogEvent(SHOW_UNLOCK_DRAW_MODE);
        }
        public static void LogClickChangeDrawMode()
        {
            LogEvent(CLICK_CHANGE_DRAW_MODE);
        }
        #endregion
    }
}

