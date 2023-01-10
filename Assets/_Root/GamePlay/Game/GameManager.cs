using Gamee.Hiuk.Common;
using Gamee.Hiuk.Component;
using Gamee.Hiuk.Data;
using Gamee.Hiuk.Game.Loader;
using Gamee.Hiuk.GamePlay;
using Gamee.Hiuk.GamePlay.UI;
using Gamee.Hiuk.Level;
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

        GamePlayManager gamePlayManager;
        GamePlayUI gamePlayUI;
        public void Init(GamePlayManager gamePlay)
        {
            this.gamePlayManager = gamePlay;
            this.gamePlayUI = gamePlay.GamePlayUI;
        }

        public void Play() 
        {
            state = EGameState.GAME_START;
            GameLoader.ActionLoadLevelCompleted = OnLoadLevelCompleted;
            LoadLevelMap();
            ShowLevelMap();
        }

        #region level
        void OnLoadLevelCompleted(GameObject goLoad) 
        {
            this.levelLoad = goLoad;
            state = EGameState.GAME_READY;
        }

        void LoadLevelMap() 
        {
            state = EGameState.GAME_LOADING;
            GameLoader.LoadLevel(GameData.LevelCurrent);
        }
        void ShowLevelMap() 
        {
            Defaut();
            StartCoroutine(WaitForShowLevelMap());
        }
        IEnumerator WaitForShowLevelMap() 
        {
            yield return new WaitUntil(() => state == EGameState.GAME_READY);
            state = EGameState.GAME_PLAYING;

            if (level != null) DestroyLevel();
            level = Instantiate(levelLoad, levelPos.transform);
            levelMap = level.GetComponentInChildren<LevelMap>();
            levelMap.Init();
            levelMap.ActionLose = GameLose;
            levelMap.ActionWin = GameWin;
        }
        void DestroyLevel() 
        {
            Destroy(level);
            levelPos.Clear();
            level = null;
        }
        #endregion

        #region game
        public void Replay() 
        {
            ShowLevelMap();
        }
        public void NextLevel() 
        {
            GameData.LevelCurrent++;
            GameLoader.levelLoadData.Uplevel();
            ShowLevelMap();
        }

        public void GameResume() 
        {
            if (state != EGameState.GAME_PAUSE) return;
            state = EGameState.GAME_PLAYING;
        }
        public void GamePause() 
        {
            state = EGameState.GAME_PAUSE;
        }
        public void GameWin() 
        {
            audioGame.PlaySound(soundWin);
        }
        public void GameLose() 
        {
            audioGame.PlaySound(soundLose);
        }
        #endregion

        void Defaut() 
        {
            gamePlayUI.DefautUI();
        }
    }
}
