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
        [SerializeField] PopupWin popupWinPrefab;
        [SerializeField] PopupLose popupLosePrefab;

        IPopupHandler popupDebugHandler;
        IPopupHandler popupWinHandler;
        IPopupHandler popupLoseHandler;
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
        public void ShowPopupWin(Action actionBackToHome, Action actionNextLevel, Action actionProcessFull)
        {
            if (popupWinHandler != null)
            {
                if (popupWinHandler.ThisGameObject.activeSelf) return;

                Display();
                return;
            }

            popupWinHandler = Instantiate(popupWinPrefab, root.transform, false);
            Display();

            void Display()
            {
                // initialize
                var popup = (PopupWin)popupWinHandler;
                Popup.Show(popupWinHandler);
                popup.Initialize(actionBackToHome, actionNextLevel, actionProcessFull);
            }
        }
        public void ShowPopupLose(Action actionBackToHome, Action actionReplayLevel, Action actionSkipLevel)
        {
            if (popupLoseHandler != null)
            {
                if (popupLoseHandler.ThisGameObject.activeSelf) return;

                Display();
                return;
            }

            popupLoseHandler = Instantiate(popupLosePrefab, root.transform, false);
            Display();

            void Display()
            {
                // initialize
                var popup = (PopupLose)popupLoseHandler;
                Popup.Show(popupLoseHandler);
                popup.Initialize(actionBackToHome, actionReplayLevel, actionSkipLevel);
            }
        }
    }
}

