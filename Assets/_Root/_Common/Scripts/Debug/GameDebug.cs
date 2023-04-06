using Gamee.Hiuk.Ads;
using Gamee.Hiuk.Data;
using Gamee.Hiuk.Game.Loader;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Debug
{
    public static class GameDebug
    {
        #region ads
        public static void SetRemoveReward(bool isRemove) { AdsManager.SetRemoveReward(isRemove); }
        public static void SetRemoveInter(bool isRemove) { AdsManager.SetRemoveInter(isRemove); }
        public static void SetRemoveAppOpen(bool isRemove) { AdsManager.SetRemoveAppOpen(isRemove); }
        public static void SetRemoveBanner(bool isRemove) { AdsManager.SetRemoveBanner(isRemove); }
        #endregion

        #region game 
        public static void AddCoin(int coin) { GameData.AddCoin(coin); }
        public static int LevelCountMax => GameLoadDataResource.LevelCount;
        public async static void TestLevel(int level)
        {
            if (level > LevelCountMax || level < 0) return;
            GameData.LevelCurrent = level;
            GameDataCache.LevelObjCache = await GameLoader.LoadLevelSellect(level);
        }
        public static bool IsDebug = false;
        #endregion
    }
}

