using Gamee.Hiuk.Data.Skin;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Popup.Daily
{
    [System.Serializable]
    public class RewardData
    {
        [SerializeField] int day;
        [SerializeField] int coin;
        [SerializeField] bool isRewardSkin;
        [SerializeField] private SkinData pinSkinData; 

        public int Day => day;
        public int DayNow => day + (GameTimeLocal.Month - 1) * 28;
        public int Coin => coin;
        public bool IsRewardSkin=> isRewardSkin && GameTimeLocal.Month == 1;
        public bool IsActive => day == GameTimeLocal.RewardDay;
        public SkinData PinSkinData => pinSkinData;
    }
}

