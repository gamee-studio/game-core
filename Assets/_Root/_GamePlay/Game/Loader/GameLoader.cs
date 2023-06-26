using Cysharp.Threading.Tasks;
using Gamee.Hiuk.Adapter;
using Gamee.Hiuk.Test;
using System;
using System.Collections.Generic;
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

        public static void UpdateLevelLoad(int level) 
        {
            var type = ELevelLoadType.LEVEL_NORMAL;

            if (level >= 5)
            {
                if (level % 5 == 0 && level >= GameConfig.LevelStartHavePuzzleCount) type = ELevelLoadType.LEVEL_PUZZLE;
                if (level % 5 == 3 || level % 5 == 4) type = ELevelLoadType.LEVEL_GP2;
            }

            levelLoadData = GameLoadDataResource.GetLevelDataCurrent(type);
        }
        public static async UniTask<GameObject> LoadLevel(int level)
        {
            var type = ELevelLoadType.LEVEL_NORMAL;

            if (level >= 5) 
            {
                if (level % 5 == 0 && level >= GameConfig.LevelStartHavePuzzleCount) type = ELevelLoadType.LEVEL_PUZZLE;
                if (level % 5 == 3 || level % 5 == 4) type = ELevelLoadType.LEVEL_GP2;
            }

            levelLoadData = GameLoadDataResource.GetLevelDataCurrent(type);
            return await GetLevel(levelLoadData.PathLevel, levelLoadData.LevelIndex);
        }
        public static async UniTask<GameObject> LoadLevelSellect(int levelSellect) 
        {
            UpdateLevelLoadData(levelSellect);
            var levelInfo = GameLoadDataResource.GetLevelInfoData(levelSellect);
            var type = levelInfo.LevelType;
            levelLoadData = GameLoadDataResource.GetLevelDataCurrent(type);
            return await GetLevel(levelLoadData.PathLevel, levelInfo.IndexLevelReal + 1);
        }
        public static void UpdateLevelLoadData(int levelSellect) 
        {
            var listLevelTypeCache = new List<ELevelLoadType>();

            for (int i = levelSellect; i >= levelSellect - 5; i--)
            {
                if (i <= 0) return;
                var levelInfo = GameLoadDataResource.GetLevelInfoData(i);
                var type = levelInfo.LevelType;
                if (!listLevelTypeCache.Contains(type)) 
                {
                    listLevelTypeCache.Add(type);
                    var levelLoad = GameLoadDataResource.GetLevelDataCurrent(type);
                    if (i == levelSellect) levelLoad.Index = levelInfo.IndexLevelReal;
                    else levelLoad.Index = levelInfo.IndexLevelReal + 1;
                }
            }
        }
    }
}

