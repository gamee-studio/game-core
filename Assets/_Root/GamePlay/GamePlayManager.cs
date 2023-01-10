using Gamee.Hiuk.Ads;
using Gamee.Hiuk.Game;
using Gamee.Hiuk.GamePlay.UI;
using Gamee.Hiuk.Level;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gamee.Hiuk.GamePlay 
{
    public class GamePlayManager : MonoBehaviour
    {
        [SerializeField] GamePlayUI gamePlayUI;
        [SerializeField] GameManager gamemanager;

        public GameManager GameManager => gamemanager;
        public GamePlayUI GamePlayUI => gamePlayUI;

        private void Awake()
        {
            Init();
        }
        public void Init()
        {
            gamemanager.ActionGameWin = OnGameWin;
            gamemanager.ActionGameLose = OnGameLose;
            gamemanager.ActionGameStart = OnGameStart;

            gamePlayUI.ActionBackHome = OnBackHome;
            gamePlayUI.ActionReplay= OnReplay;
            gamePlayUI.ActionSkip = OnSkip;
        }

        private void Start()
        {
            gamemanager.Run();
        }

        #region game
        void OnGameWin(LevelMap level) 
        {
            gamePlayUI.MoveUI();
        }
        void OnGameLose(LevelMap level) 
        {
            gamePlayUI.MoveUI();
        }

        void OnGameStart() 
        {
            gamePlayUI.DefautUI();
        }
        #endregion

        #region ui
        void OnBackHome() 
        {
            SceneManager.LoadScene(1);
        }
        void OnReplay() 
        {
            gamemanager.Replay();
        }
        void OnSkip() 
        {
            AdsManager.ShowReard((isWatched) =>
            {
                if(isWatched) gamemanager.SkipLevel();
            });
        }
        #endregion
    }
}

