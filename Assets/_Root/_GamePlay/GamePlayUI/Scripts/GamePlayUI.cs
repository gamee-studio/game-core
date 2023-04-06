using Gamee.Hiuk.Popup;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Gamee.Hiuk.Debug;

namespace Gamee.Hiuk.GamePlay.UI 
{
    public class GamePlayUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI txtLevel;
        [SerializeField] TextMeshProUGUI txtLevelDesc;
        [SerializeField] List<ObjMoveUI> listObjectMove;
        [SerializeField] GameObject btnSkip;
        [SerializeField] GameObject btnBack;
        PopupManager popupManager;

        public Action ActionBackHome;
        public Action ActionReplay;
        public Action<bool> ActionNextLevel;
        public Action ActionSkipLevel;
        public Action ActionBackLevel;
        public Action<bool> ActionProcessFull;

        public void Init()
        {
            popupManager = PopupManager.Instance;
            btnBack.gameObject.SetActive(GameDebug.IsDebug);
        }
        public void Start()
        {
            popupManager.HideAll();
        }
        public void DefautUI() 
        {
            foreach(var obj in listObjectMove) 
            {
                obj.Defaut();
            }
        }
        public void MoveUI() 
        {
            foreach (var obj in listObjectMove)
            {
                obj.Move();
            }
        }
        public void UpdateLevelDescText(string desc) 
        {
            txtLevelDesc.text = desc;
        }
        public void UpdateTextLevel(int level) 
        {
            txtLevel.text = string.Format("LEVEL {0}", level);
        }
        public void ShowButtonSkip(bool isShow) 
        {
            btnSkip.gameObject.SetActive(isShow);
        }
        public void ShowPopupWin() 
        {
            popupManager.ShowPopupWin(BackHome, NextLevelOnWin, OnFullProcess);
        }
        public void ShowPopupLose()
        {
            popupManager.ShowPopupLose(BackHome, ReplayLevel, NextLevelOnLose);
        }
        public void ShowPopupRate(Action actionClose = null) 
        {
            popupManager.ShowPopupRate(actionClose);
        }
        void NextLevelOnWin() 
        {
            ActionNextLevel?.Invoke(false);
        }
        void NextLevelOnLose()
        {
            ActionNextLevel?.Invoke(true);
        }
        public void ReplayLevel()
        {
            ActionReplay?.Invoke();
        }
        public void SkipLevel()
        {
            ActionSkipLevel?.Invoke();
        }
        public void BackLevel() 
        {
            ActionBackLevel?.Invoke();
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

