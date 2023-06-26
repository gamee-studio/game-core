using UnityEngine;
using TMPro;
using Gamee.Hiuk.Data;
using DG.Tweening;

public class CoinDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtCoin;
    [SerializeField] float timeCoinChangeValue = 1f;
    [SerializeField] Ease ease = Ease.Linear;

    int coinCurrent = 0;
    private void OnEnable()
    {
        coinCurrent = GameData.CoinCurrent;
        txtCoin.text = string.Format("{0}", coinCurrent);
        GameData.ActionCoinValueChange += OnChangeCoinValue;

    }
    private void OnDisable()
    {
        GameData.ActionCoinValueChange -= OnChangeCoinValue;
    }
    void OnChangeCoinValue(int coin)
    {
        if (coinCurrent == GameData.CoinCurrent) return;
        int valueCache = 0;
        var t = DOTween.To(x => valueCache = (int)x, coinCurrent, GameData.CoinCurrent, timeCoinChangeValue).SetEase(ease);
        t.OnUpdate(() =>
        {
            txtCoin.text = string.Format("{0}", valueCache);
        });
        t.OnComplete(() =>
        {
            coinCurrent = GameData.CoinCurrent;
        });
    }
}
