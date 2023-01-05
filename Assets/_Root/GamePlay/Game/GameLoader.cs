using Cysharp.Threading.Tasks;
using Gamee.Hiuk.Adapter;
using Gamee.Hiuk.Test;
using UnityEngine;

namespace Gamee.Hiuk.Game 
{
    public static class GameLoader
    {
        const string LEVEL = "Level_{0}";
        public static async UniTask<GameObject> GetLevel(int index)
        {
            if (GameTest.IsTest) return GameTest.LevelAsset;
            return await AddressablesAdapter.GetAsset(string.Format(LEVEL, index));
        }
    }
}

