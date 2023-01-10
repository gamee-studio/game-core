using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Debug 
{
    public static class GameDebug
    {
        private static bool isRemoveInterAds;
        private static bool isRemoveRewardAds;
        public static bool IsRemoveInterAds
        {
            get => isRemoveInterAds;
            set => isRemoveInterAds = value;
        }
        public static bool IsRemoveRewardAds
        {
            get => isRemoveRewardAds;
            set => isRemoveRewardAds = value;
        }
    }
}

