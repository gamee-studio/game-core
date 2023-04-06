using Gamee.Hiuk.Common;
using Gamee.Hiuk.Component;
using Gamee.Hiuk.Data;
using Gamee.Hiuk.FirebseAnalytic;
using Gamee.Hiuk.Game.Loader;
using Gamee.Hiuk.Level;
using System;
using System.Collections;
using UnityEngine;

namespace Gamee.Hiuk.Game
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] EGameState state;
        [SerializeField] Transform levelPos;

        [Header("Audio")]
        [SerializeField] AudioComponent audioGame;
        [SerializeField] Sound soundWin;
        [SerializeField] Sound soundLose;
        LevelMap levelMap;
        GameObject level;
        GameObject levelLoad;

        public Action<LevelMap> ActionGameWin;
        public Action<LevelMap> ActionGameLose;
        public Action ActionGameStart;
        public Action<LevelMap> ActionGameLoadLevelComplete;
        public LevelMap LevelMap => levelMap;

        #region game
        public void Replay()
        {
            FirebaseAnalytic.LogLevelReplay(GameLoader.levelLoadData.LevelNameCurrent + GameData.LevelNameCurrent);
            Clear();
            LoadLevelMap();
            GameStart();
        }
        public void NextLevelData()
        {
            GameData.LevelCurrent++;
            GameLoader.levelLoadData.Uplevel();
        }
        public void BackLevelData() 
        {
            if (GameData.LevelCurrent == 1) return;
            GameData.LevelCurrent--;
            GameLoader.levelLoadData.DownLevel();
        }
        public void NextLevel(bool isSkip = false)
        {
            if (isSkip)
            {
                FirebaseAnalytic.LogLevelSkip(GameLoader.levelLoadData.LevelNameCurrent + GameData.LevelNameCurrent);
                NextLevelData();
            }
            LoadLevelMap();
            GameStart();
        }
        public async void BackLevel() 
        {
            //BackLevelData();
            GameData.LevelCurrent--;
            GameDataCache.LevelObjCache = await GameLoader.LoadLevelSellect(GameData.LevelCurrent);
            LoadLevelMap();
            GameStart();
        }
        public void Run()
        {
            LoadLevelMap();
            GameStart();
        }
        void GameStart()
        {
            FirebaseAnalytic.LogLevelStart(GameLoader.levelLoadData.LevelNameCurrent + GameData.LevelNameCurrent);
            StartCoroutine(WaitForShowLevelMap());
            ActionGameStart?.Invoke();
        }
        public void GamePlay()
        {
            state = EGameState.GAME_PLAYING;
        }
        public void GameResume()
        {
            if (state != EGameState.GAME_PAUSE) return;
            GamePlay();
        }
        public void GamePause()
        {
            state = EGameState.GAME_PAUSE;
        }
        public void GameWin()
        {
            if (state == EGameState.GAME_WIN) return;
            state = EGameState.GAME_WIN;
            FirebaseAnalytic.LogLevelCompleted(GameLoader.levelLoadData.LevelNameCurrent + GameData.LevelNameCurrent);
            audioGame.PlaySound(soundWin);
            NextLevelData();
            LoadLevelMap();
            ActionGameWin?.Invoke(levelMap);
        }
        public void GameLose()
        {
            if (state == EGameState.GAME_LOSE) return;
            state = EGameState.GAME_LOSE;
            FirebaseAnalytic.LogLevelFailed(GameLoader.levelLoadData.LevelNameCurrent + GameData.LevelNameCurrent);
            audioGame.PlaySound(soundLose);
            ActionGameLose?.Invoke(levelMap);
        }
        #endregion

        #region level
        async void LoadLevelMap()
        {
            state = EGameState.GAME_LOADING;
            if (levelLoad == null && GameDataCache.LevelObjCache != null)
            {
                levelLoad = GameDataCache.LevelObjCache;
                state = EGameState.GAME_READY;
            }
            else
            {
                var goLoad = await GameLoader.LoadLevel(GameData.LevelCurrent);
                this.levelLoad = goLoad;
                GameDataCache.LevelObjCache = goLoad;
                state = EGameState.GAME_READY;
            }
        }
        IEnumerator WaitForShowLevelMap()
        {
            yield return new WaitUntil(() => state == EGameState.GAME_READY);

            if (level != null) DestroyLevel();
            level = Instantiate(levelLoad, levelPos.transform);
            levelMap = level.GetComponentInChildren<LevelMap>();
            levelMap.Init();
            levelMap.ActionLose = GameLose;
            levelMap.ActionWin = GameWin;

            ActionGameLoadLevelComplete?.Invoke(levelMap);
            GamePlay();
        }
        void DestroyLevel()
        {
            Destroy(level);
            levelPos.Clear();
            level = null;
        }
        public void Clear()
        {
            levelMap.Clear();
        }
        #endregion
    }
}
