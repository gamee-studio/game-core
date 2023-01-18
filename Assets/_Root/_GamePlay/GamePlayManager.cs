using Gamee.Hiuk.Ads;
using Gamee.Hiuk.Component;
using Gamee.Hiuk.Game;
using Gamee.Hiuk.GamePlay.UI;
using Gamee.Hiuk.Level;
using System;
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
        [Header("Time Delay")]
        [SerializeField, Range(0, 5)] float timeDelayWin = 1;
        [SerializeField, Range(0, 5)] float timeDelayLose = .5f;

        [Header("Audio"), SerializeField] AudioComponent audioGamePlay;
        [SerializeField] Sound soundBg;

        public GameManager GameManager => gamemanager;
        public GamePlayUI GamePlayUI => gamePlayUI;

        private void Awake()
        {
            Init();
        }
        public void Init()
        {
            gamePlayUI.Init();

            gamemanager.ActionGameWin = OnGameWin;
            gamemanager.ActionGameLose = OnGameLose;
            gamemanager.ActionGameStart = OnGameStart;

            gamePlayUI.ActionBackHome = OnBackHome;
            gamePlayUI.ActionReplay= OnReplay;
            gamePlayUI.ActionSkip = OnSkip;
        }

        private void Start()
        {
            PlaySoundBG();
            gamemanager.Run();
        }
        void PlaySoundBG() 
        {
            audioGamePlay.PlaySoundBackGround(soundBg);
        }
        public IEnumerator DelayTime(float time = 0.5f, Action actionCompleted = null)
        {
            yield return new WaitForSeconds(time);
            actionCompleted?.Invoke();
        }
        #region game
        void OnGameWin(LevelMap level) 
        {
            gamePlayUI.MoveUI();
            StartCoroutine(DelayTime(timeDelayWin, () =>
            {
                gamePlayUI.ShowPopupWin();
            }));
        }
        void OnGameLose(LevelMap level) 
        {
            gamePlayUI.MoveUI();
            StartCoroutine(DelayTime(timeDelayLose, () =>
            {
                gamePlayUI.ShowPopupLose();
            }));
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

