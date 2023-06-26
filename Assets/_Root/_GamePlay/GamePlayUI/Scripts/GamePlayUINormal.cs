using UnityEngine;
using TMPro;
using Gamee.Hiuk.Debug;

namespace Gamee.Hiuk.GamePlay.UI 
{
    public class GamePlayUINormal : GamePlayUI
    {
        [SerializeField] TextMeshProUGUI txtLevelDesc;
        [SerializeField] GameObject btnSkip;
        [SerializeField] GameObject btnBack;

        public override void Init()
        {
            btnBack.gameObject.SetActive(GameDebug.IsDebug);
            base.Init();
        }
        public override void ShowPopupWin(int coinBonus = 0)
        {
            popupManager.ShowPopupWin(BackHome, NextLevelOnWin, OnFullProcess, coinBonus);
        }
        public override void ShowPopupLose(bool isShowSkip = false)
        {
            popupManager.ShowPopupLose(BackHome, ReplayLevel, NextLevelOnLose);
        }
    }
}

