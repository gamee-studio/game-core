namespace Gamee.Hiuk.FirebaseRemoteConfig
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;

    public class FirebaseRemoteConfig : MonoBehaviour
    {
        private const string IS_ADMOB = "IS_ADMOB";

        private const string IS_REWARDED_INTERSTITIAL = "IS_REWARDED_INTERSTITIAL";
        private const string IS_SHOW_CROSS_ADS = "IS_SHOW_CROSS_ADS";
        private const string INTER_AD_SHOW_COUNT = "INTER_AD_SHOW_COUNT";
        private const string INTER_AD_SHOW_COUNT_IN_NEW_APP = "INTER_AD_SHOW_COUNT_IN_NEW_APP";
        private const string TIME_INTER_AD_SHOW_DELAY = "TIME_INTER_AD_SHOW_DELAY";
        private const string TIME_INTER_AD_SHOW_LOSE_DELAY = "TIME_INTER_AD_SHOW_LOSE_DELAY";
        private const string IS_INTER_ADS_LOSE = "IS_INTER_ADS_LOSE";
        private const string VERSION_APP = "VERSION_APP";
        private const string VERSION_APP_IOS = "VERSION_APP_IOS";
        private const string DESCRIPTION_APP = "DESCRIPTION_APP";

        private const string AUTO_START_GAME = "AUTO_START_GAME";
        private const string IS_SHOW_DAILY_REWARD = "IS_SHOW_DAILY_REWARD";
        private const string IS_SHOW_INTER_ADS_BEFORE_WIN = "IS_SHOW_INTER_ADS_BEFORE_WIN";
        private const string PROCESS_COUNT = "PROCESS_COUNT";
        private readonly Dictionary<string, object> defaults = new Dictionary<string, object>();

        public void Init()
        {
            defaults.Add(IS_ADMOB, "false");

            defaults.Add(IS_REWARDED_INTERSTITIAL, "false");
            defaults.Add(IS_SHOW_CROSS_ADS, "false");
            defaults.Add(INTER_AD_SHOW_COUNT, "3");
            defaults.Add(TIME_INTER_AD_SHOW_DELAY, "25");
            defaults.Add(TIME_INTER_AD_SHOW_LOSE_DELAY, "25");
            defaults.Add(IS_INTER_ADS_LOSE, "false");
            defaults.Add(INTER_AD_SHOW_COUNT_IN_NEW_APP, "3");
            defaults.Add(VERSION_APP, "1.0");
            defaults.Add(VERSION_APP_IOS, "1.0");
            defaults.Add(DESCRIPTION_APP, "New Update");
            defaults.Add(AUTO_START_GAME, "false");
            defaults.Add(IS_SHOW_DAILY_REWARD, "false");
            defaults.Add(IS_SHOW_INTER_ADS_BEFORE_WIN, "false");
            defaults.Add(PROCESS_COUNT, "10");
            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);

            FetchDataAsync();
        }

        private Task FetchDataAsync()
        {
            Task fetchTask = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
            return fetchTask.ContinueWith(FetchComplete);
        }

        private void FetchComplete(Task fetchTask)
        {
            if (fetchTask.IsCanceled)
            {
                Debug.Log("Fetch canceled.");
            }
            else if (fetchTask.IsFaulted)
            {
                Debug.Log("Fetch encountered an error.");
            }
            else if (fetchTask.IsCompleted)
            {
                Debug.Log("Fetch completed successfully!");
            }

            var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
            switch (info.LastFetchStatus)
            {
                case Firebase.RemoteConfig.LastFetchStatus.Success:
                    Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
                    Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
                    info.FetchTime));
                    break;
                case Firebase.RemoteConfig.LastFetchStatus.Failure:
                    switch (info.LastFetchFailureReason)
                    {
                        case Firebase.RemoteConfig.FetchFailureReason.Error:
                            Debug.Log("Fetch failed for unknown reason");
                            break;
                        case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                            Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
                            break;
                    }
                    break;
                case Firebase.RemoteConfig.LastFetchStatus.Pending:
                    Debug.Log("Latest Fetch call still pending.");
                    break;
            }

/*            Config.IsAdmob = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(IS_ADMOB).StringValue);

            Config.InterstitialAdShowCount = int.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(INTER_AD_SHOW_COUNT).StringValue);
            Config.InterstitialAdFirstShowCount = int.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(INTER_AD_SHOW_COUNT_IN_NEW_APP).StringValue);

            Config.TimeInterAdShow = int.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(TIME_INTER_AD_SHOW_DELAY).StringValue);
            Config.TimeInterAdShowLose = int.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(TIME_INTER_AD_SHOW_LOSE_DELAY).StringValue);
            Config.IsShowInterAdsLose = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(IS_INTER_ADS_LOSE).StringValue);

#if UNITY_IOS
            GameData.VersionApp = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(VERSION_APP_IOS).StringValue;
#elif UNITY_ANDROID || UNITY_EDITOR
            GameData.VersionApp = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(VERSION_APP).StringValue;
#endif

            GameData.DescritptionApp = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(DESCRIPTION_APP).StringValue;
            Config.AutoStartGame = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(AUTO_START_GAME).StringValue);
            Config.IsAutoShowDailyReward = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(IS_SHOW_DAILY_REWARD).StringValue);
            Config.IsShowCrossAds = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(IS_SHOW_CROSS_ADS).StringValue);
            Config.IsShowInterAdsBeforeWin = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(IS_SHOW_INTER_ADS_BEFORE_WIN).StringValue);
            Config.ProcessCount = int.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(PROCESS_COUNT).StringValue);
            Config.LinkFB = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(LINK_FACEBOOK).StringValue;
            Config.IsOnVibration = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(IS_ON_VIBRATION).StringValue);
            Config.IsHaveEvent = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(IS_HAVE_EVENT).StringValue);
            Config.IsHaveBonus = bool.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue(IS_HAVE_BONUS).StringValue);*/
            }
        }
    }
