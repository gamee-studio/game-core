using Gamee.Hiuk.Adapter;
using Gamee.Hiuk.Ads;
using Gamee.Hiuk.Common;
using Gamee.Hiuk.Data;
using Gamee.Hiuk.FirebaseRemoteConfig;
using Gamee.Hiuk.UI.Helper;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gamee.Hiuk.Popup 
{
    public class PopupWin : UniPopup
    {
        [SerializeField] int coinValue;
        [SerializeField] int watchVideoValue;
        [SerializeField] TextMeshProUGUI txtCoin;
        [SerializeField] GameObject objCoinDisplay;
        [SerializeField] GameObject btnNextLevel;
        [SerializeField] GameObject btnWatchVideo;
        [SerializeField] LuckySpin luckySpin;
        [Header("Process")]
        [SerializeField] ProcessUI processUI;
        [SerializeField] int processIndex;
        [SerializeField] float processRunTime = 1f;
        [SerializeField] GitWin gitWin;

        private Action actionBackToHome;
        private Action actionNextLevel;
        private Action<bool> actionProcessFull;
        private int coin;
        private int coinBonus;
        private bool isSellected = false;
        Coroutine coroutine;
        public int ProcessValueCurrent 
        {
            get => PlayerPrefsAdapter.GetInt("popup_win_process_value", 0);
            set => PlayerPrefsAdapter.SetInt("popup_win_process_value", value);
        }
        public void Initialize(Action actionBackToHome, Action actionNextLevel, Action<bool> actionProcessFull, int coinBonus = 0)
        {
            this.actionBackToHome = actionBackToHome;
            this.actionNextLevel = actionNextLevel;
            this.actionProcessFull = actionProcessFull;

            DefautUI();

            this.coinBonus = coinBonus;
            coin = coinValue + coinBonus;
            txtCoin.text = coin.ToString();

            gitWin.Init();
            gitWin.Idle();

            ProcessRun();
            luckySpin.Initialize();
            luckySpin.ActionSelectItem = OnSellectItem;

            btnNextLevel.DoDelay(2f);
        }

        public void NextLevel() 
        {
            if (isSellected) return;
            isSellected = true;
            coroutine = StartCoroutine(WaitSellectButtonTime());
            AddCoin(coin);
        }
        public void WatchVideo()
        {
            if (!AdsManager.IsInterAdsReady) return;
            if (isSellected) return;
            isSellected = true;
            coroutine = StartCoroutine(WaitSellectButtonTime());

            luckySpin.Pause((item) =>
            {
                luckySpin.ActionSelectItem -= OnSellectItem;
                AdsManager.ShowReard((isWatched) =>
                {
                    if (isWatched)
                    {
                        AddCoin(coin * watchVideoValue);
                    }
                    else DefautUI();

                    luckySpin.ActionSelectItem = OnSellectItem;
                }, () =>
                {
                    luckySpin.Reset();
                    DefautUI();
                });
            });
        }
        public void BackToHome() 
        {
            actionBackToHome?.Invoke();
            Close();
        }

        void AddCoin(int coin) 
        {
            btnNextLevel.gameObject.SetActive(false);
            btnWatchVideo.gameObject.SetActive(false);

            CoinGeneration.Generate(null, () =>
            {
                GameData.AddCoin(coin);
                actionNextLevel?.Invoke();
                Close();
            }, txtCoin.gameObject, objCoinDisplay);
        }
        void ProcessRun() 
        {
            isSellected = true;
            processUI.UpdateUI(ProcessValueCurrent, processIndex);
            ProcessValueCurrent++;
            processUI.Run(ProcessValueCurrent, processIndex, processRunTime);
            if(ProcessValueCurrent == processIndex) 
            {
                gitWin.Open();
            }
            processUI.ActionFull = OnProcessFull;
            processUI.ActionCompleted = () =>
            {
                isSellected = false;
            };
        }
        void OnProcessFull(bool isFull) 
        {
            if (isFull)
            {
                actionProcessFull?.Invoke(true);
                ProcessValueCurrent = 0;
            }
            else actionProcessFull?.Invoke(false);
        }
        void DefautUI() 
        {
            isSellected = false;
            btnNextLevel.gameObject.SetActive(true);
            btnWatchVideo.gameObject.SetActive(true);
        }
        void OnSellectItem(int value) 
        {
            watchVideoValue = value;
            txtCoin.text = string.Format("{0}", coin * watchVideoValue);
        }
        IEnumerator WaitSellectButtonTime()
        {
            yield return new WaitForSeconds(.5f);
            isSellected = false;
            luckySpin.Reset();
        }
        private void OnDisable()
        {
            if (coroutine != null) StopCoroutine(coroutine);
        }
    }
}

