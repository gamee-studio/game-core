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
        [Header("Process")]
        [SerializeField] ProcessUI processUI;
        [SerializeField] int processIndex;
        private Action actionBackToHome;
        private Action actionNextLevel;
        private Action actionProcessFull;
        private int coin;
        private int coinBonus;

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

            coin = coinValue + coinBonus;
            txtCoin.text = coin.ToString();

            ProcessRun();
        }

        public void NextLevel() 
        {
            AddCoin(coin);
        }
        public void WatchVideo()
        {
            AdsManager.ShowReard((isWatched) =>
            {
                if (isWatched) 
                {
                    AddCoin(coin * watchVideoValue);
                }
            });
        }
        public void BackToHome() 
        {
            actionBackToHome?.Invoke();
        }

        void AddCoin(int coin) 
        {
            CoinGeneration.Generate(null, () =>
            {
                GameData.AddCoin(coin);
                actionNextLevel?.Invoke();
            });
        }
        void ProcessRun() 
        {
            processUI.Run(ProcessValueCurrent, processIndex);
            ProcessValueCurrent++;
            processUI.Run(10, processIndex, .5f);
            processUI.ActionFull = OnProcessFull;
        }
        void OnProcessFull() 
        {
            actionProcessFull?.Invoke();
            ProcessValueCurrent = 0;
        }
    }
}

