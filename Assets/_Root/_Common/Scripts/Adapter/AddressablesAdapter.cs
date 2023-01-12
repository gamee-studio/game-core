using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Gamee.Hiuk.Adapter 
{
    public static class AddressablesAdapter
    {
        public static async UniTask<GameObject> GetAsset(string assetName)
        {
            return await Addressables.LoadAssetAsync<GameObject>(assetName);
        }
    }
}

