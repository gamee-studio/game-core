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

        public static async UniTask<GameObject> LoadLevel(int level)
        {
            var type = ELevelLoadType.LEVEL_NORMAL;
            levelLoadData = GameLoadDataResource.GetLevelDataCurrent(type);
            return await GetLevel(levelLoadData.PathLevel, levelLoadData.LevelIndex);
        }
        public static async UniTask<GameObject> LoadLevelSellect(int levelTest) 
        {
            var levelNormalData = GameLoadDataResource.GetLevelDataCurrent(ELevelLoadType.LEVEL_NORMAL);
            levelNormalData.SetIndex(levelTest - 1);
            return await LoadLevel(levelTest);
        }
    }
}

