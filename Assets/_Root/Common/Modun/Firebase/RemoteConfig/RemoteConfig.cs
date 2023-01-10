using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.FirebaseRemoteConfig 
{
    public static class RemoteConfig
    {
        public static bool IsAdmob = false;
        public static bool IsAutoStartGame = true;
        public static bool IsAutoShowDailyReward = false;
        public static bool IsShowInterAdsBeforeWin = true;

        public static bool IsShowInterAdsLose = true;
        public static int InterstitialAdShowCount = 3;
        public static int InterstitialAdFirstShowCount = 3;
        public static int TimeInterAdShow = 25;
        public static int TimeInterAdShowLose = 25;

        public static string VersionApp = "1.0";
        public static string DescritptionApp = "New Update";
    }
}
