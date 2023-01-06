using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.FirebaseRemoteConfig 
{
    public static class RemoteConfig
    {
        public static bool IsAdmob;
        public static bool IsAutoStartGame;
        public static bool IsAutoShowDailyReward;
        public static bool IsShowInterAdsBeforeWin;

        public static bool IsShowInterAdsLose;
        public static int InterstitialAdShowCount;
        public static int InterstitialAdFirstShowCount;
        public static int TimeInterAdShow;
        public static int TimeInterAdShowLose;

        public static string VersionApp;
        public static string DescritptionApp;
    }
}

