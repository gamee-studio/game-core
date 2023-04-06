using Cysharp.Threading.Tasks;
using Gamee.Hiuk.Adapter;
using Gamee.Hiuk.Test;
using System;
using UnityEngine;

namespace Gamee.Hiuk.Game.Loader
{
    public static class GameLoader
    {
        public static LevelLoadData levelLoadData;

        public static void Init()
        {
            GameLoadDataResource.InitData();
        }

        public static async UniTask<GameObject> GetLevel(string levelPath, int index)
        {
            if (GameTest.IsTest) return GameTest.LevelAsset;
            return await AddressablesAdapter.GetAsset(string.Format(levelPath, index));
        }
        public static async UniTask<GameObject> GetLevel(int level)
        {
            if (GameTest.IsTest) return GameTest.LevelAsset;
            string levelFullPath = GameLoadDataResource.GetLevelInfoData(level).levelRealName;
            return await AddressablesAdapter.GetAsset(levelFullPath);
        }

        public static async UniTask<GameObject> LoadLevel(int level)
        {
            var type = ELevelLoadType.LEVEL_NORMAL;

            levelLoadData = GameLoadDataResource.GetLevelDataCurrent(type);
            return await GetLevel(levelLoadData.PathLevel, levelLoadData.LevelIndex);
        }
        public static async UniTask<GameObject> LoadLevelSellect(int levelSellect) 
        {
            UpdateLevelLoadData(levelSellect);
            var levelInfo = GameLoadDataResource.GetLevelInfoData(levelSellect);
            var type = levelInfo.LevelType;
            levelLoadData = GameLoadDataResource.GetLevelDataCurrent(type);
            return await GetLevel(levelLoadData.PathLevel, levelLoadData.LevelIndex);
        }
        public static void UpdateLevelLoadData(int levelSellect) 
        {
            for (int i = levelSellect - 1; i <= levelSellect; i++)
            {
                var levelInfo = GameLoadDataResource.GetLevelInfoData(i);
                var type = levelInfo.LevelType;
                var levelLoad = GameLoadDataResource.GetLevelDataCurrent(type);
                levelLoad.Index = levelInfo.IndexLevelReal;
            }
        }
    }
}

