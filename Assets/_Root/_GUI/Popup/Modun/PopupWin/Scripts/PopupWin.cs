using Gamee.Hiuk.Adapter;
using Gamee.Hiuk.Ads;
using Gamee.Hiuk.Common;
using Gamee.Hiuk.Data;
using Gamee.Hiuk.UI.Helper;
using System;
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

        public int ProcessValueCurrent 
        {
            get => PlayerPrefsAdapter.GetInt("popup_win_process_value", 0);
            set => PlayerPrefsAdapter.SetInt("popup_win_process_value", value);
        }
        public void Initialize(Action actionBackToHome, Action actionNextLevel, Action<bool> actionProcessFull)
        {
            this.actionBackToHome = actionBackToHome;
            this.actionNextLevel = actionNextLevel;
            this.actionProcessFull = actionProcessFull;

            DefautUI();

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
            AddCoin(coin);
        }
        public void WatchVideo()
        {
            if (!AdsManager.IsInterAdsReady) return;
            if (isSellected) return;
            isSellected = true;

            luckySpin.Pause((item) =>
            {
                luckySpin.ActionSelectItem -= OnSellectItem;
                AdsManager.ShowReard((isWatched) =>
                {
                    if (isWatched)
                    {
                        AddCoin(coin * watchVideoValue);
                    }
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
    }
}

