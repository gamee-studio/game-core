namespace Gamee.Hiuk.FirebaseRemoteConfig 
{
    using Gamee.Hiuk.Ads;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using DG.Tweening;
    public class RemoteConfigAdapter : MonoBehaviour
    {
        private const string IS_ADMOB = "IS_ADMOB";
        private const string IS_AUTO_START_GAME = "IS_AUTO_START_GAME";
        private const string IS_SHOW_DAILY_REWARD = "IS_SHOW_DAILY_REWARD";
        private const string IS_SHOW_INTER_ADS_BEFORE_WIN = "IS_SHOW_INTER_ADS_BEFORE_WIN";

        private const string IS_INTER_ADS_LOSE = "IS_INTER_ADS_LOSE";
        private const string INTER_AD_SHOW_COUNT = "INTER_AD_SHOW_COUNT";
        private const string INTER_AD_SHOW_COUNT_IN_NEW_APP = "INTER_AD_SHOW_COUNT_IN_NEW_APP";
        private const string TIME_INTER_AD_SHOW_DELAY = "TIME_INTER_AD_SHOW_DELAY";
        private const string TIME_INTER_AD_SHOW_LOSE_DELAY = "TIME_INTER_AD_SHOW_LOSE_DELAY";

        private const string VERSION_APP = "VERSION_APP";
        private const string VERSION_APP_IOS = "VERSION_APP_IOS";
        private const string DESCRIPTION_APP = "DESCRIPTION_APP";

        private const string IS_SHOW_LEVEL_DESCRIPTION = "IS_SHOW_LEVEL_DESCRIPTION";

        private readonly Dictionary<string, object> defaults = new Dictionary<string, object>();

        public void Defaut() 
        {
            defaults.Add(IS_ADMOB, "false");

            defaults.Add(INTER_AD_SHOW_COUNT, "3");
            defaults.Add(TIME_INTER_AD_SHOW_DELAY, "25");
            defaults.Add(TIME_INTER_AD_SHOW_LOSE_DELAY, "25");
            defaults.Add(IS_INTER_ADS_LOSE, "false");
            defaults.Add(INTER_AD_SHOW_COUNT_IN_NEW_APP, "3");
            defaults.Add(VERSION_APP, "1.0");
            defaults.Add(VERSION_APP_IOS, "1.0");
            defaults.Add(DESCRIPTION_APP, "New Update");
            defaults.Add(IS_AUTO_START_GAME, "true");
            defaults.Add(IS_SHOW_DAILY_REWARD, "false");
            defaults.Add(IS_SHOW_INTER_ADS_BEFORE_WIN, "false");
            defaults.Add(IS_SHOW_LEVEL_DESCRIPTION, "true");

            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);
        }

        public void FetchData()
        {
            RemoteConfig.IsAdmob = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(IS_ADMOB).StringValue);
            RemoteConfig.IsAutoStartGame = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(IS_AUTO_START_GAME).StringValue);
            RemoteConfig.IsAutoShowDailyReward = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(IS_SHOW_DAILY_REWARD).StringValue);
            RemoteConfig.IsShowInterAdsBeforeWin = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(IS_SHOW_INTER_ADS_BEFORE_WIN).StringValue);

            RemoteConfig.IsShowInterAdsLose = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(IS_INTER_ADS_LOSE).StringValue);
            RemoteConfig.InterAdShowCount = int.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(INTER_AD_SHOW_COUNT).StringValue);
            RemoteConfig.InterAdFirstShowCount = int.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(INTER_AD_SHOW_COUNT_IN_NEW_APP).StringValue);
            RemoteConfig.TimeInterAdShowWin = int.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(TIME_INTER_AD_SHOW_DELAY).StringValue);
            RemoteConfig.TimeInterAdShowLose = int.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(TIME_INTER_AD_SHOW_LOSE_DELAY).StringValue);

#if UNITY_IOS
            RemoteConfig.VersionApp = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(VERSION_APP_IOS).StringValue;
#elif UNITY_ANDROID || UNITY_EDITOR
            RemoteConfig.VersionApp = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(VERSION_APP).StringValue;
#endif
            RemoteConfig.DescritptionApp = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(DESCRIPTION_APP).StringValue;
            RemoteConfig.IsShowLevelDescription = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(IS_SHOW_LEVEL_DESCRIPTION).StringValue);
            // init ads
            DOTween.Sequence().SetDelay(.15f).OnComplete(() =>
            {
                AdsManager.Config(RemoteConfig.IsAdmob);
                AdsManager.Init();
            });
        }
    }
}

