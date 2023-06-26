using Gamee.Hiuk.Common;
using Gamee.Hiuk.Component;
using Gamee.Hiuk.Level;
using System;
using System.Collections;
using UnityEngine;

namespace Gamee.Hiuk.Game
{
    public abstract class GameManager : MonoBehaviour
    {
        [SerializeField] protected EGameState state;
        [SerializeField] Transform levelPos;

        [Header("Audio")]
        [SerializeField] AudioComponent audioGame;
        [SerializeField] Sound soundWin;
        [SerializeField] Sound soundLose;
        protected LevelMap levelMap;
        protected GameObject level;
        protected GameObject levelLoad;

        public Action<LevelMap> ActionGameWin;
        public Action<LevelMap> ActionGameLose;
        public Action ActionGameStart;
        public Action ActionGameStartControl;
        public Action<LevelMap> ActionGameLoadLevelComplete;
        public LevelMap LevelMap => levelMap;

        #region game
        public void Replay()
        {
            LogEventLevelReplay();
            Clear();
            LoadLevelMap();
            GameStart();
        }
        public abstract void LogEventLevelReplay();
        public abstract void NextLevelData();
        public abstract void BackLevelData();

        public virtual void NextLevel(bool isSkip = false)
        {
            if (isSkip)
            {
                LogEventLevelSkip();
                NextLevelData();
            }
            LoadLevelMap();
            GameStart();
        }
        public abstract void LogEventLevelSkip();

        public void BackLevel() 
        {
            BackLevelData();
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
            LogEventStartLevel();
            StartCoroutine(WaitForShowLevelMap());
            ActionGameStart?.Invoke();
        }
        public abstract void LogEventStartLevel();

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
            audioGame.PlaySound(soundWin);
            LogEventLevelWin();
            NextLevelData();
            LoadLevelMap();
            ActionGameWin?.Invoke(levelMap);
        }
        public abstract void LogEventLevelWin(); 

        public void GameLose()
        {
            if (state == EGameState.GAME_LOSE) return;
            state = EGameState.GAME_LOSE;
            LogEventLevelLose();
            audioGame.PlaySound(soundLose);
            ActionGameLose?.Invoke(levelMap);
        }
        public void GameStartControl() 
        {
            ActionGameStartControl?.Invoke();
        }
        public abstract void LogEventLevelLose();
        #endregion

        #region level
        public abstract void LoadLevelMap();
        IEnumerator WaitForShowLevelMap()
        {
            yield return new WaitUntil(() => state == EGameState.GAME_READY);

            if (level != null) DestroyLevel();
            level = Instantiate(levelLoad, levelPos.transform);
            levelMap = level.GetComponentInChildren<LevelMap>();
            levelMap.Init();
            levelMap.ActionLose = GameLose;
            levelMap.ActionWin = GameWin;
            levelMap.ActionStartControl = GameStartControl;
            levelMap.Play();

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
