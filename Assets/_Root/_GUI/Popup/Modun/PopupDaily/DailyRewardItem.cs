using Gamee.Hiuk.Data;
using Spine.Unity;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Gamee.Hiuk.Adapter;
using Gamee.Hiuk.Button;
using System;
using Gamee.Hiuk.FirebseAnalytic;

namespace Gamee.Hiuk.Popup.Daily
{
    public class DailyRewardItem : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txtDay;
        [SerializeField] TextMeshProUGUI txtCoin;
        [SerializeField] GameObject active;
        [SerializeField] GameObject coinGroup;
        [SerializeField] GameObject block;
        [SerializeField] GameObject skinGroup;
        [SerializeField] GameObject check;
        [SerializeField] UniButton btnClick;

        RewardData rewardData;

        public bool IsHasReceiveGift => rewardData != null ? GameTimeLocal.IsHasReward && rewardData.IsActive : false;
        public bool IsReceived 
        {
            get => PlayerPrefsAdapter.GetBool($"{rewardData.DayNow}"); 
            set => PlayerPrefsAdapter.SetBool($"{rewardData.DayNow}", value); }

        public Action actionUpdate;
        public Action actionClick;
        public RewardData RewardData => rewardData;
        public void Init(RewardData reward)
        {
            this.rewardData = reward;
            UpdateUI();
        }

        public void UpdateUI()
        {
            active.SetActive(IsHasReceiveGift);
            check.SetActive(IsReceived);
            btnClick.interactable = IsHasReceiveGift;
            txtDay.text = "Day " + rewardData.Day;
            block.SetActive(IsReceived);

            if (rewardData.IsRewardSkin)
            {
                coinGroup.SetActive(false);
            }
            else
            {
                coinGroup.SetActive(!IsReceived);
                skinGroup.SetActive(false);
                txtCoin.text = $"+{rewardData.Coin}";
            }
        }

        public void Claim()
        {
            if (rewardData.DayNow == 1) FirebaseAnalytic.LogDailyClaimDay1();
            if (rewardData.DayNow == 3) FirebaseAnalytic.LogDailyClaimDay3();
            if (rewardData.DayNow == 7) FirebaseAnalytic.LogDailyClaimDay7();
            GameTimeLocal.IsHasReward = false;
            IsReceived = true;
            actionUpdate?.Invoke();
            UpdateUI();
        }

        public void Click() 
        {
            actionClick?.Invoke();
        }
    }
}

