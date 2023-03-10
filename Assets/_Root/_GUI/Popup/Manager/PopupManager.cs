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
        private Popup popup;
        public Popup Popup => popup ?? (popup = new Popup());

        [SerializeField] PopupDebug popupDebugPrefab;
        [SerializeField] PopupWin popupWinPrefab;
        [SerializeField] PopupLose popupLosePrefab;
        [SerializeField] PopupSetting popupSettingPrefab;
        [SerializeField] PopupUpdate popupUpdatePrefab;
        [SerializeField] PopupRate popupRatePrefab;

        IPopupHandler popupDebugHandler;
        IPopupHandler popupWinHandler;
        IPopupHandler popupLoseHandler;
        IPopupHandler popupSettingHandler;
        IPopupHandler popupUpdateHandler;
        IPopupHandler popupRateHandler;
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
        public void ShowPopupWin(Action actionBackToHome, Action actionNextLevel, Action<bool> actionProcessFull)
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
        public void ShowPopupSetting(Action actionClose)
        {
            if (popupSettingHandler != null)
            {
                if (popupSettingHandler.ThisGameObject.activeSelf) return;

                Display();
                return;
            }

            popupSettingHandler = Instantiate(popupSettingPrefab, root.transform, false);
            Display();

            void Display()
            {
                // initialize
                var popup = (PopupSetting)popupSettingHandler;
                Popup.Show(popupSettingHandler);
                popup.Initialize(actionClose);
            }
        }
        public void ShowPopupUpdate(Action<bool> actionClose, string strDescription, string strVersionUpdate)
        {
            if (popupUpdateHandler != null)
            {
                if (popupUpdateHandler.ThisGameObject.activeSelf) return;

                Display();
                return;
            }

            popupUpdateHandler = Instantiate(popupUpdatePrefab, root.transform, false);
            Display();

            void Display()
            {
                // initialize
                var popup = (PopupUpdate)popupUpdateHandler;
                Popup.Show(popupUpdateHandler);
                popup.Initialize(actionClose, strDescription, strVersionUpdate);
            }
        }
        public void ShowPopupRate(Action actionClose)
        {
            if (popupRateHandler != null)
            {
                if (popupRateHandler.ThisGameObject.activeSelf) return;

                Display();
                return;
            }

            popupRateHandler = Instantiate(popupRatePrefab, root.transform, false);
            Display();

            void Display()
            {
                // initialize
                var popup = (PopupRate)popupRateHandler;
                Popup.Show(popupRateHandler);
                popup.Initialize(actionClose);
            }
        }
    }
}

