using Gamee.Hiuk.Game;
using Gamee.Hiuk.Popup;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.GamePlay.UI 
{
    public class GamePlayUI : MonoBehaviour
    {
        GamePlayManager gamePlayManager;
        GameManager gameManager;
        PopupManager popupManager;

        public Action ActionBackHome;
        public Action ActionReplay;
        public Action<bool> ActionNextLevel;
        public Action<bool> ActionProcessFull;
        public void Init(GamePlayManager gamePlay)
        {
            this.gamePlayManager = gamePlay;
            this.gameManager = gamePlay.GameManager;
        }
        public void Init()
        {
            popupManager = PopupManager.Instance;
        }
        public void DefautUI() { }
        public void MoveUI() { }
        public void ShowPopupWin() 
        {
            popupManager.ShowPopupWin(BackHome, NextLevel, OnFullProcess);
        }
        public void ShowPopupLose()
        {
            popupManager.ShowPopupLose(BackHome, ReplayLevel, SkipLevel);
        }
        public void ShowPopupRate() 
        {
            popupManager.ShowPopupRate(null);
        }
        public void NextLevel() 
        {
            ActionNextLevel?.Invoke(false);
        }
        public void ReplayLevel()
        {
            ActionReplay?.Invoke();
        }
        public void SkipLevel()
        {
            ActionNextLevel?.Invoke(true);
        }
        public void OnFullProcess(bool isFull) 
        {
            ActionProcessFull?.Invoke(isFull);
        }
        public void BackHome()
        {
            ActionBackHome?.Invoke();
        }
        public void RePlay() 
        {
            ActionReplay?.Invoke();
        }
    }
}

