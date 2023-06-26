using Gamee.Hiuk.Component;
using Gamee.Hiuk.Game.Input;
using Gamee.Hiuk.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamee.Hiuk.Ads;
using Gamee.Hiuk.Data;
using Gamee.Hiuk.FirebaseRemoteConfig;
using Gamee.Hiuk.Level;
using UnityEngine.SceneManagement;
using System;
using Gamee.Hiuk.Data.Skin;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField] protected EGamePlayState state;
    [SerializeField] protected GamePlayUI gamePlayUI;
    [SerializeField] protected TouchManager touchManager;
    [SerializeField] protected GameManager gameManager;
    [SerializeField, SceneProperty] protected string sceneHomeName;
    [Header("Time Delay")]
    [SerializeField, Range(0, 5)] protected float timeDelayWin = 0f;
    [SerializeField, Range(0, 5)] protected float timeDelayLose = 0f;

    [Header("Audio"), SerializeField] protected AudioComponent audioGamePlay;
    [SerializeField] protected Sound soundBg;

    protected virtual GameManager gameManagerCurrent => gameManager;
    protected Sound soundBgCurrent;

    public GameManager GameManager => gameManagerCurrent;
    public GamePlayUI GamePlayUI => gamePlayUI;
    public LevelMap LevelMap => levelMap;
    public bool IsPlaying => state == EGamePlayState.GAMEPLAY_PLAYING;

    protected bool isPreLevelWin = true;
    protected Coroutine coroutineDelayWin;
    protected Coroutine coroutineDelayLose;
    protected LevelMap levelMap;
    protected GameObject objFollow;

    protected virtual void Awake()
    {
        Init();
    }
    protected virtual void Init()
    {
        gamePlayUI.Init();

        gameManagerCurrent.ActionGameWin = OnGameWin;
        gameManagerCurrent.ActionGameLose = OnGameLose;
        gameManagerCurrent.ActionGameStart = OnGameStart;
        gameManagerCurrent.ActionGameStartControl = OnGameStartControl;
        gameManagerCurrent.ActionGameLoadLevelComplete = OnGameLoadLevelComplete;

        gamePlayUI.ActionBackHome = OnBackHome;
        gamePlayUI.ActionReplay = OnReplayLevel;
        gamePlayUI.ActionNextLevel = OnNextLevel;
        gamePlayUI.ActionSkipLevel = OnSkipLevel;
        gamePlayUI.ActionBackLevel = OnBackLevel;
        gamePlayUI.ActionProcessFull = OnProcessFull;

        if (objFollow == null) objFollow = new GameObject("Obj-Follow");
        gamePlayUI.HideAllPopup();
    }
    private void Start()
    {
        state = EGamePlayState.GAMEPLAY_START;
        gameManagerCurrent.Run();
    }
    #region game
    protected virtual void OnGameWin(LevelMap level)
    {
        AdsManager.HideBanner();
        isPreLevelWin = true;
        GameDataCache.InterAdCountCurrent++;
        gamePlayUI.MoveUI();
        if (RemoteConfig.IsShowInterAdsBeforeWin)
        {
            ShowInter();
        }
        coroutineDelayWin = StartCoroutine(DelayTime(timeDelayWin, () =>
        {
            gamePlayUI.ShowPopupWin(level.CoinBonus);
        }));
    }
    protected virtual void OnGameLose(LevelMap level)
    {
        AdsManager.HideBanner();
        isPreLevelWin = false;
        if (RemoteConfig.IsShowInterAdsLose) GameDataCache.InterAdCountCurrent++;
        gamePlayUI.MoveUI();
        coroutineDelayLose = StartCoroutine(DelayTime(timeDelayLose, () =>
        {
            ShowPopupLose();
        }));
    }
    protected virtual void ShowPopupLose(bool isShowSkip = false) 
    {
        gamePlayUI.ShowPopupLose(isShowSkip);
    }
    protected virtual void OnGameStart()
    {
        touchManager.Run(false);

        AdsManager.ShowBanner();
        if (!RemoteConfig.IsShowInterAdsBeforeWin)
        {
            ShowInter();
        }
        gamePlayUI.DefautUI();
        FxManager.ReleaseAll();
        UpdateTextLevel();
    }
    protected virtual void UpdateTextLevel() 
    {
        gamePlayUI.UpdateTextLevel($"Level {GameData.LevelCurrent}");
    }
    protected virtual void OnGameStartControl()
    {
        touchManager.Run(true);
    }
    protected virtual void OnGameLoadLevelComplete(LevelMap levelMap) 
    {
        this.levelMap = levelMap;
        levelMap.ActionWatting = OnLevelWaitting;
        state = EGamePlayState.GAMEPLAY_PLAYING;
        PlaySoundBG(soundBg);
    }
    protected void PlaySoundBG(Sound sound)
    {
        if (soundBgCurrent == sound) return;
        soundBgCurrent = sound;
        audioGamePlay.PlaySoundBackGround(soundBgCurrent);
    }
    protected virtual void OnLevelWaitting()
    {
    }
    protected void StopCoroutineDelay()
    {
        if (coroutineDelayLose != null) StopCoroutine(coroutineDelayLose);
        if (coroutineDelayWin != null) StopCoroutine(coroutineDelayWin);
    }
    public IEnumerator DelayTime(float time = 0.5f, Action actionCompleted = null)
    {
        yield return new WaitForSeconds(time);
        actionCompleted?.Invoke();
    }
    protected void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    #endregion

    #region ui
    protected virtual void OnBackHome()
    {
        AdsManager.HideBanner();
        gameManagerCurrent.Clear();
        StopCoroutineDelay();
        FxManager.ReleaseAll();
        SceneManager.LoadScene(sceneHomeName);
    }
    protected virtual void OnReplayLevel()
    {
        if (!IsPlaying) return;
        StopCoroutineDelay();
        ShowInterInReplay();
        gameManagerCurrent.Replay();
    }
    protected virtual void ShowInterInReplay() 
    {
        if (RemoteConfig.IsShowInterAdsReplay)
        {
            isPreLevelWin = false;
            GameDataCache.InterAdCountCurrent++;
            ShowInter();
        }
    }
    protected virtual void OnNextLevel(bool isSkip = false)
    {
        gameManagerCurrent.NextLevel(isSkip);
    }
    protected virtual void OnSkipLevel()
    {
        if (!AdsManager.IsRewardAdsReady) return;
        AdsManager.ShowReard((isWatched) =>
        {
            if (isWatched) OnNextLevel(true);
        });
    }
    protected virtual void OnBackLevel()
    {
        gameManagerCurrent.BackLevel();
    }
    protected virtual void OnProcessFull(bool isFull)
    {
        if (isFull)
        {
            // to do
        }
        else
        {
            if (IsShowRate()) gamePlayUI.ShowPopupRate();
        }
    }
    protected bool IsShowRate()
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
        if (!IsShowInter) return;
        gameManagerCurrent.GamePause();
        AdsManager.ShowInter(() =>
        {
            ResetInterShowTime();
            gameManagerCurrent.GamePlay();
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
}
