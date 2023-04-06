using Gamee.Hiuk.Data;
using Gamee.Hiuk.Debug;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gamee.Hiuk.Popup 
{
    public class PopupDebug : UniPopup
    {
        [SerializeField] Toggle tgUseDebug;
        [SerializeField] GameObject groupDebug;
        [Header("ads")]
        [SerializeField] Toggle tgRemoveReward;
        [SerializeField] Toggle tgRemoveInter;
        [SerializeField] Toggle tgRemoveBanner;
        [Header("game")]
        [SerializeField] private TMP_InputField ifEnterCoin;
        [SerializeField] private TMP_InputField ifEnterLevel;
        [SerializeField] Toggle tgUnlockAllPuzzle;

        private Action actionclose;
        private Action actionUnlockAllPuzzle;
        private int coin;
        private int level;

        public void Initialize(Action actionClose, Action actionUnlockAllPuzzle)
        {
            this.actionclose = actionClose;
            this.actionUnlockAllPuzzle = actionUnlockAllPuzzle;

            ifEnterCoin.contentType = TMP_InputField.ContentType.IntegerNumber;
            ifEnterLevel.contentType = TMP_InputField.ContentType.IntegerNumber;

            OnUseDebug(tgUseDebug.isOn);
            tgUseDebug.onValueChanged.AddListener((isOn) => 
            {
                OnUseDebug(isOn);
            });

            GameDebug.SetRemoveReward(GameData.IsRemoveRewardAds);
            GameDebug.SetRemoveInter(GameData.IsRemoveInterAds);
            GameDebug.SetRemoveBanner(GameData.IsRemoveBannerAds);

            tgUnlockAllPuzzle.isOn = false;
        }

        void OnUseDebug(bool isUse)
        {
            GameDebug.IsDebug = isUse;
            groupDebug.gameObject.SetActive(isUse);
        }
        public void OkBabi()
        {
            if (tgUseDebug.isOn) 
            {
                GameDebug.SetRemoveReward(tgRemoveReward.isOn);
                GameDebug.SetRemoveInter(tgRemoveInter.isOn);
                GameDebug.SetRemoveBanner(tgRemoveBanner.isOn);

                if (int.TryParse(ifEnterCoin.text, out coin))
                {
                    GameDebug.AddCoin(coin);
                }
                if (int.TryParse(ifEnterLevel.text, out level))
                {
                    GameDebug.TestLevel(level);
                }

                if (tgUnlockAllPuzzle.isOn) actionUnlockAllPuzzle?.Invoke();
            }

            Back();
        }

        void Back() 
        {
            actionclose?.Invoke();
            Close();
        }
    }
}

