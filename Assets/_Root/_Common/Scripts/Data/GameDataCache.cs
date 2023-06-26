using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Data 
{
    public static class GameDataCache
    {
        public static GameObject LevelObjCache { set; get; }
        public static int InterAdCountCurrent { set; get; } = 0;
        public static DateTime TimeAtInterShowWin { set; get; } = new DateTime();
        public static DateTime TimeAtInterShowLose { set; get; } = new DateTime();
        public static bool IsJustShowRewardAds { set; get; } = false;
        public static bool IsPlayChallengeMode { set; get; } = false;
    }
}

