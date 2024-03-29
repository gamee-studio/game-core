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
        public static bool IsShowInterAdsReplay = true;
        public static int InterAdShowCount = 3;
        public static int InterAdFirstShowCount = 3;
        public static int TimeInterAdShowWin = 25;
        public static int TimeInterAdShowLose = 25;

        public static string VersionApp = "1.0.0";
        public static string DescritptionApp = "New Update";

        public static bool IsShowLevelDescription = true;
        public static string LinkFB;

        public static bool IsShowDailyMissionInGame = false;
        public static bool IsShowDailyMissionInWin = false;

        public static bool IsShowButtonFBInGitCode = false;
    }
}

