using Gamee.Hiuk.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.GamePlay.UI 
{
    public class GamePlayUI : MonoBehaviour
    {
        GamePlayManager gamePlayManager;
        GameManager gameManager;
        public void Init(GamePlayManager gamePlay)
        {
            this.gamePlayManager = gamePlay;
            this.gameManager = gamePlay.GameManager;
        }
        public void DefautUI() { }
        public void MoveUI() { }

        public void BackHome() { }
        public void RePlay() 
        {
            gameManager.Replay();
        }
        public void Skip() { }
    }
}

