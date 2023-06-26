using Gamee.Hiuk.Adapter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamee.Hiuk.Data.Skin 
{
    public abstract class SkinData : ScriptableObject
    {
        [SerializeField] Sprite icon;
        [SerializeField] Sprite iconUI;
        [SerializeField] Sprite iconUIDisable;
        [SerializeField] string description;
        [SerializeField] bool isBuyCoin;
        [SerializeField] bool isWatchVideo;
        [SerializeField] bool isDailyReward;
        [SerializeField] bool isGiftCode;
        [SerializeField] bool isGitBox;
        [SerializeField] bool isRandom;
        [SerializeField] int coin = 0;
        [SerializeField] string code;
        [SerializeField] bool isManyWatchVideo;
        [SerializeField] int watchVideoCount = 2;
        [GUID, SerializeField] string id;
        [HideInInspector, SerializeField] bool isHas;

        public Sprite Icon => icon;
        public Sprite IconUI => iconUI == null ? icon : iconUI;
        public Sprite IconUIDisable => iconUIDisable == null ? iconUI : iconUIDisable;
        public abstract string GetSkinName();
        public string SkinName => GetSkinName();
        public string Description => description;
        public int Coin => coin;
        public bool IsBuyCoin => isBuyCoin;
        public bool IsWatchVideo => isWatchVideo;
        public bool IsDailyReward => isDailyReward;
        public bool IsGiftCode => isGiftCode;
        public bool IsGiftBox => isGitBox;
        public bool IsRandom => isRandom;
        public string Code => code;
        public bool IsManyWatchVideo => isManyWatchVideo;
        public int WatchVideoCount => watchVideoCount;
        public string ID => id;
        public string TextManyWatchVideoCount => $"{WatchCount}/{watchVideoCount}";
        public bool IsCanBySkin => WatchCount >= watchVideoCount;
        public bool IsHas
        {
            get => PlayerPrefsAdapter.GetBool(id + "is_has");
            set => PlayerPrefsAdapter.SetBool(id + "is_has", value);
        }
        public int WatchCount 
        {
            get => PlayerPrefsAdapter.GetInt(id + "watch_count");
            set => PlayerPrefsAdapter.SetInt(id + "watch_count", value);
        }
    }
}

