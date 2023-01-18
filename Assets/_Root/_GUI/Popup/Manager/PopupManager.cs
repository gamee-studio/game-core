using Gamee.Hiuk.Component;
using Gamee.Hiuk.Pattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Gamee.Hiuk.Popup
{
    public class PopupManager : Singleton<PopupManager>
    {
        [SerializeField] GameObject root;
        [SerializeField] CoinGeneration coinGeneration;
        private Popup popup;
        public Popup Popup => popup ?? (popup = new Popup());

        [SerializeField] PopupDebug popupDebugPrefab;

        IPopupHandler popupDebugHandler;
        public void GenerateCoin(Action moveOneCoinDone, Action moveAllCoinDone, GameObject from = null, GameObject to = null, int numberCoin = -1)
        {
            coinGeneration.GenerateCoin(moveOneCoinDone, moveAllCoinDone, from, to, numberCoin);
        }
        public void ShowPopupDebug(Action actionClose)
        {
            if (popupDebugHandler != null)
            {
                if (popupDebugHandler.ThisGameObject.activeSelf) return;

                Display();
                return;
            }

            popupDebugHandler = Instantiate(popupDebugPrefab, root.transform, false);
            Display();

            void Display()
            {
                // initialize
                var popup = (PopupDebug)popupDebugHandler;
                Popup.Show(popupDebugHandler);
                popup.Initialize(actionClose);
            }
        }
        public void ShowPopupWin() { }
        public void ShowPopupLose() { }
    }
}

