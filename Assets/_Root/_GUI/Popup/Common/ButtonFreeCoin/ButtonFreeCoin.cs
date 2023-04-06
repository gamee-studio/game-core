using TMPro;
using UnityEngine;
using Gamee.Hiuk.Ads;
using System;

public class ButtonFreeCoin : MonoBehaviour
{
    [SerializeField] int coinValue = 500;
    [SerializeField] TextMeshProUGUI txtCoin;

    public Action<int> ActionWatchCompleted;
    public void Awake()
    {
        txtCoin.text = $"+{coinValue}";
    }

    public void Watch() 
    {
        if (!AdsManager.IsRewardAdsReady) return;
        AdsManager.ShowReard((isWatched) =>
        {
            if (isWatched) ActionWatchCompleted?.Invoke(coinValue);
        });
    }
}
