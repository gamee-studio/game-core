using Gamee.Hiuk.Adapter;
using Gamee.Hiuk.Ads;
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
        [SerializeField] GameObject btnNextLevel;
        [SerializeField] GameObject btnWatchVideo;
        [Header("Process")]
        [SerializeField] ProcessUI processUI;
        [SerializeField] int processIndex;
        private Action actionBackToHome;
        private Action actionNextLevel;
        private Action actionProcessFull;
        private int coin;
        private int coinBonus;
        private bool isSellected = false;

        public int ProcessValueCurrent 
        {
            get => PlayerPrefsAdapter.GetInt("popup_win_process_value", 0);
            set => PlayerPrefsAdapter.SetInt("popup_win_process_value", value);
        }
        public void Initialize(Action actionBackToHome, Action actionNextLevel, Action actionProcessFull)
        {
            this.actionBackToHome = actionBackToHome;
            this.actionNextLevel = actionNextLevel;
            this.actionProcessFull = actionProcessFull;

            DefautUI();

            coin = coinValue + coinBonus;
            txtCoin.text = coin.ToString();

            ProcessRun();
        }

        public void NextLevel() 
        {
            if (isSellected) return;
            isSellected = true;
            AddCoin(coin);
        }
        public void WatchVideo()
        {
            if (isSellected) return;
            isSellected = true;

            AdsManager.ShowReard((isWatched) =>
            {
                if (isWatched) 
                {
                    AddCoin(coin * watchVideoValue);
                }
            }, ()=> 
            {
                DefautUI();
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
            });
        }
        void ProcessRun() 
        {
            processUI.Run(ProcessValueCurrent, processIndex);
            ProcessValueCurrent++;
            processUI.Run(ProcessValueCurrent, processIndex, .5f);
            processUI.ActionFull = OnProcessFull;
        }
        void OnProcessFull() 
        {
            actionProcessFull?.Invoke();
            ProcessValueCurrent = 0;
        }
        void DefautUI() 
        {
            isSellected = false;
            btnNextLevel.gameObject.SetActive(true);
            btnWatchVideo.gameObject.SetActive(true);
        }
    }
}

