using Gamee.Hiuk.Ads;
using Gamee.Hiuk.Component;
using Gamee.Hiuk.Data;
using Gamee.Hiuk.FirebaseRemoteConfig;
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
        [SerializeField, SceneProperty] string sceneHomeName;
        [Header("Time Delay")]
        [SerializeField, Range(0, 5)] float timeDelayWin = 1;
        [SerializeField, Range(0, 5)] float timeDelayLose = .5f;

        [Header("Audio"), SerializeField] AudioComponent audioGamePlay;
        [SerializeField] Sound soundBg;

        public GameManager GameManager => gamemanager;
        public GamePlayUI GamePlayUI => gamePlayUI;

        private bool isPreLevelWin = true;
        private Coroutine coroutineDelayWin;
        private Coroutine coroutineDelayLose;

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
            gamePlayUI.ActionReplay = OnReplayLevel;
            gamePlayUI.ActionNextLevel = OnNextLevel;
            gamePlayUI.ActionProcessFull = OnProcessFull;
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

        #region game
        void OnGameWin(LevelMap level)
        {
            isPreLevelWin = true;
            GameDataCache.InterAdCountCurrent++;

            gamePlayUI.MoveUI();
            if (RemoteConfig.IsShowInterAdsBeforeWin)
            {
                ShowInter();
            }

            coroutineDelayWin = StartCoroutine(DelayTime(timeDelayWin, () =>
            {
                gamePlayUI.ShowPopupWin();
            }));
        }
        void OnGameLose(LevelMap level)
        {
            isPreLevelWin = false;
            if (RemoteConfig.IsShowInterAdsLose) GameDataCache.InterAdCountCurrent++;

            gamePlayUI.MoveUI();
            coroutineDelayLose = StartCoroutine(DelayTime(timeDelayLose, () =>
            {
                gamePlayUI.ShowPopupLose();
            }));
        }
        void OnGameStart()
        {
            if (IsShowInter && !RemoteConfig.IsShowInterAdsBeforeWin)
            {
                ShowInter();
            }

            gamePlayUI.DefautUI();
        }
        public IEnumerator DelayTime(float time = 0.5f, Action actionCompleted = null)
        {
            yield return new WaitForSeconds(time);
            actionCompleted?.Invoke();
        }
        void StopCoroutineDelay()
        {
            if (coroutineDelayLose != null) StopCoroutine(coroutineDelayLose);
            if (coroutineDelayWin != null) StopCoroutine(coroutineDelayWin);
        }
        #endregion

        #region inter ads show
        public void ResetInterShowTime()
        {
            GameDataCache.TimeAtInterShowWin = DateTime.Now;
            GameDataCache.TimeAtInterShowLose = DateTime.Now;
        }
        public bool CheckTimeWin()
        {
            if ((DateTime.Now - GameDataCache.TimeAtInterShowWin).TotalSeconds >= RemoteConfig.TimeInterAdShowWin)
            {
                GameDataCache.InterAdCountCurrent = 0;
                return true;
            }
            return false;
        }
        public bool CheckTimeLose()
        {
            if ((DateTime.Now - GameDataCache.TimeAtInterShowLose).TotalSeconds >= RemoteConfig.TimeInterAdShowLose)
            {
                GameDataCache.InterAdCountCurrent = 0;
                return true;
            }
            return false;
        }
        public void ShowInter()
        {
            gamemanager.GamePause();
            AdsManager.ShowInter(() =>
            {
                ResetInterShowTime();
                gamemanager.GamePlay();
            });
        }
        public bool IsShowInter
        {
            get
            {
                if (GameDataCache.IsJustShowRewardAds)
                {
                    GameDataCache.IsJustShowRewardAds = false;
                    return false;
                }
                if (!AdsManager.IsInterAdsReady) return false;
                if (RemoteConfig.InterAdFirstShowCount > GameData.LevelCurrent) return false;
                if (GameDataCache.InterAdCountCurrent < RemoteConfig.InterAdShowCount) return false;

                if (RemoteConfig.IsShowInterAdsLose)
                {
                    if (isPreLevelWin)
                    {
                        return CheckTimeWin();
                    }
                    else
                    {
                        return CheckTimeLose();
                    }
                }
                else return CheckTimeWin();
            }
        }
        #endregion

        #region ui
        void OnBackHome()
        {
            SceneManager.LoadScene(sceneHomeName);
        }
        void OnReplayLevel()
        {
            StopCoroutineDelay();
            gamemanager.Replay();
        }
        void OnNextLevel(bool isSkip = false)
        {
            gamemanager.NextLevel(isSkip);
        }
        void OnProcessFull(bool isFull)
        {
            if (isFull) { }
            else
            {
                if (IsShowRate()) gamePlayUI.ShowPopupRate();
            }
        }
        bool IsShowRate()
        {
            if (GameData.IsShowedRate) return false;

            if (GameData.LevelCurrent >= GameConfig.LevelShowRateCount)
            {
                if (GameData.LevelShowedRateCount < GameConfig.LevelShowRateCount) GameData.LevelShowedRateCount = GameConfig.LevelShowRateCount;
                if (GameData.LevelCurrent >= GameData.LevelShowedRateCount)
                {
                    GameData.LevelShowedRateCount += GameConfig.LevelShowRateNextValue;
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}

