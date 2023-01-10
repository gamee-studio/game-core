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

        public static Action<GameObject> ActionLoadLevelCompleted;
        public static void Init()
        {
            GameLoadDataResource.InitData();
        }

        public static async UniTask<GameObject> GetLevel(string levelPath, int index)
        {
            if (GameTest.IsTest) return GameTest.LevelAsset;
            return await AddressablesAdapter.GetAsset(string.Format(levelPath, index));
        }

        public static async void LoadLevel(int level)
        {
            var type = ELevelLoadType.LEVEL_NORMAL;
            levelLoadData = GameLoadDataResource.GetLevelDataCurrent(type);

            var levelObj = await GetLevel(levelLoadData.PathLevel, levelLoadData.LevelIndex);
            ActionLoadLevelCompleted?.Invoke(levelObj);
        }
    }
}

