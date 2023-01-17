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

        private Action actionclose;
        private int coin;
        private int level;

        public void Initialize(Action actionClose = null)
        {
            this.actionclose = actionClose;

            ifEnterCoin.contentType = TMP_InputField.ContentType.IntegerNumber;
            ifEnterLevel.contentType = TMP_InputField.ContentType.IntegerNumber;

            tgUseDebug.onValueChanged.AddListener((isOn) => 
            {
                OnUseDebug(isOn);
            });
        }

        void OnUseDebug(bool isUse)
        {
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

