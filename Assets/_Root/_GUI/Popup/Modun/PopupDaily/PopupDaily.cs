using Gamee.Hiuk.Ads;
using Gamee.Hiuk.Button;
using Gamee.Hiuk.Data;
using Gamee.Hiuk.FirebseAnalytic;
using Gamee.Hiuk.Popup.Daily;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gamee.Hiuk.Popup
{
    public class PopupDaily : UniPopup
    {
        [SerializeField] List<DailyRewardItem> listRewardItem;
        [SerializeField] List<WeekRewardData> listWeekRewardData;

        [SerializeField] private TextMeshProUGUI txtCoinWatchVideo;
        [SerializeField] private TextMeshProUGUI txtWatchValue;
        [SerializeField] private UniButton btnWatchVideo;
        [SerializeField] private UniButton btnClaim;
        [SerializeField] private Transform iconCoinDisplay;

        private DailyRewardItem rewardCurrent;
        private RewardData rewardData => rewardCurrent.RewardData;
        WeekRewardData WeekRewardData => listWeekRewardData[GameTimeLocal.RewardWeek - 1];
        private Action actionClose;
        
        public void Initialize(Action actionClose)
        {
            this.actionClose = actionClose;

            for (int i = 0; i< listRewardItem.Count; i++) 
            {
                listRewardItem[i].Init(WeekRewardData.rewards[i]);

                if (WeekRewardData.rewards[i].IsActive)
                {
                    rewardCurrent = listRewardItem[i];

                    if (rewardCurrent.IsHasReceiveGift)
                    {
                        rewardCurrent.actionClick = OnClaim;
                    }
                }
            }
            
            UpdateUI();
        }

        public void WatchVideo()
        {
            if (!AdsManager.IsRewardAdsReady) return;
            AdsManager.ShowReard((isWatched) =>
            {
                if (isWatched) 
                {
                    FirebaseAnalytic.LogDailyClaimBonus();
                    rewardCurrent.Claim();
                    AddCoin(rewardData.Coin * GameConfig.WatchVideoValue);
                }
            });
        }
        public void OnClaim()
        {
            rewardCurrent.Claim();
            if (rewardData.IsRewardSkin)
            {
                ReceiveSkin();
                return;
            }

            AddCoin(rewardData.Coin);
        }

        void UpdateUI()
        {
            btnClaim.gameObject.SetActive(GameTimeLocal.IsHasReward);
            if (rewardData.IsRewardSkin) 
            {
                btnWatchVideo.gameObject.SetActive(false);
                return;
            }

            btnWatchVideo.gameObject.SetActive(GameTimeLocal.IsHasReward);
            txtCoinWatchVideo.text = $"{rewardData.Coin * GameConfig.WatchVideoValue}";
            txtWatchValue.text = $"X{GameConfig.WatchVideoValue} Coin";
        }
        
        private void AddCoin(int coin)
        {
            UpdateUI();
            CoinGeneration.Generate(null, () =>
            {
                GameData.AddCoin(coin);
            }, rewardCurrent.gameObject, iconCoinDisplay.gameObject);
        }
        
        void ReceiveSkin()
        {
            var skinData = rewardCurrent.RewardData.PinSkinData;
            skinData.IsHas = true;
            UpdateUI();
        }
        public void Back()
        {
            actionClose?.Invoke();
            Close();
        }
    }
}

