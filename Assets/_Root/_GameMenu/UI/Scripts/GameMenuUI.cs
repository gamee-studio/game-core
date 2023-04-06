using DG.Tweening;
using Gamee.Hiuk.Data;
using Gamee.Hiuk.Popup;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace Gamee.Hiuk.GameMenu.UI 
{
    public class GameMenuUI : MonoBehaviour
    {
        [SerializeField] GameObject btnDebug;
        [SerializeField] GameObject btnRemoveAds;

        [SerializeField] List<ObjMoveUI> listObjectMove;
        PopupManager popupManager;
        public Action ActionStartGame;
        public Action<bool> ActionUpdateMusic;
        public Action ActionRemoveAds;

        public void Init()
        {
            popupManager = PopupManager.Instance;
        }
        public void Start()
        {
            btnDebug.gameObject.SetActive(GameConfig.IsShowDebug);
            popupManager.HideAll();
        }
        public void StartGame()
        {
            ActionStartGame?.Invoke();
        }

        public void ShowPopupDebug()
        {
            popupManager.ShowPopupDebug(null, ()=> 
            {

            });
        }
        public void ShowPopupSetting()
        {
            popupManager.ShowPopupSetting(ActionUpdateMusic, null);
        }
        public void ShowPopupNewUpdate(Action<bool> actionClose, string strDescription, string strVersionUpdate)
        {
            popupManager.ShowPopupUpdate(actionClose, strDescription, strVersionUpdate);
        }

        public void ShowPopupDaily()
        {
            popupManager.ShowPopupDaily(()=> 
            {
            });
        }
        public void BuyRemoveAds() 
        {
            ActionRemoveAds?.Invoke();
        }

        public void UpdateButtonRemoveAds(bool isShow) 
        {
            btnRemoveAds.gameObject.SetActive(isShow);
        }
        public void DefautUI()
        {
            foreach (var obj in listObjectMove)
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
    }
}

