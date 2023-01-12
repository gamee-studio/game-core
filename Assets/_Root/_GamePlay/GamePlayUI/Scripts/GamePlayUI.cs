using Gamee.Hiuk.Game;
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

        public Action ActionBackHome;
        public Action ActionReplay;
        public Action ActionSkip;
        public void Init(GamePlayManager gamePlay)
        {
            this.gamePlayManager = gamePlay;
            this.gameManager = gamePlay.GameManager;
        }
        public void DefautUI() { }
        public void MoveUI() { }

        public void BackHome()
        {
            ActionBackHome?.Invoke();
        }
        public void RePlay() 
        {
            ActionReplay?.Invoke();
        }
        public void Skip()
        {
            ActionSkip?.Invoke();
        }
    }
}

