using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using Gamee.Hiuk.Component;
using Gamee.Hiuk.Pattern;

public class CoinGeneration : Singleton<CoinGeneration>
{
    [SerializeField] private GameObject root;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject from;
    [SerializeField] private GameObject to;
    [SerializeField] private int numberCoin;
    [SerializeField] private int delay;
    [SerializeField] private float durationNear;
    [SerializeField] private float durationTarget;
    [SerializeField] private Ease easeNear;
    [SerializeField] private Ease easeTarget;
    [SerializeField] private float circleSize = 1;
    [SerializeField] private float scale = 1;
    [SerializeField] Sound soundCoinMove;
    [SerializeField] AudioComponent audioCoin;
    private int numberCoinMoveDone;
    private System.Action moveOneCoinDone;
    private System.Action moveAllCoinDone;

    public void SetFromGameObject(GameObject from)
    {
        this.from = from;
    }

    public void SetToGameObject(GameObject to)
    {
        this.to = to;
    }

    public async void GenerateCoin(System.Action moveOneCoinDone, System.Action moveAllCoinDone, GameObject from = null, GameObject to = null, int numberCoin = -1)
    {
        audioCoin.PlaySound(soundCoinMove);
        this.moveOneCoinDone = moveOneCoinDone;
        this.moveAllCoinDone = moveAllCoinDone;
        this.from = from == null ? this.from : from;
        this.to = to == null ? this.to : to;
        this.numberCoin = numberCoin < 0 ? this.numberCoin : numberCoin;
        numberCoinMoveDone = 0;
        for (int i = 0; i < this.numberCoin; i++)
        {
            await Task.Delay(Random.Range(0, delay));
            GameObject coin = Instantiate(coinPrefab, root.transform);
            coin.transform.localScale = Vector3.one * scale;
            coin.transform.position = this.from.transform.position;
            MoveCoin(coin);
        }
    }

    private void MoveCoin(GameObject coin)
    {

        MoveToNear(coin).OnComplete(() =>
        {
            MoveToTarget(coin).OnComplete(() =>
            {
                numberCoinMoveDone++;
                Destroy(coin);
                moveOneCoinDone?.Invoke();
                if (numberCoinMoveDone >= numberCoin)
                {
                    moveAllCoinDone?.Invoke();
                }
            });
        });
    }

    private DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> MoveTo(Vector3 endValue, GameObject coin, float duration, Ease ease)
    {
        return coin.transform.DOMove(endValue, duration).SetEase(ease);
    }

    private DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> MoveToNear(GameObject coin)
    {
        return MoveTo(coin.transform.position + (Vector3)Random.insideUnitCircle * circleSize, coin, durationNear, easeNear);
    }

    private DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> MoveToTarget(GameObject coin)
    {
        return MoveTo(to.transform.position, coin, durationTarget, easeTarget);
    }

    #region static api
    public static void Generate(System.Action moveOneCoinDone, System.Action moveAllCoinDone, GameObject from = null, GameObject to = null, int numberCoin = -1)
    {
        Instance.GenerateCoin(moveOneCoinDone, moveAllCoinDone, from, to, numberCoin);
    }
    #endregion
}
