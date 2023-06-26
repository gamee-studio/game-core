using Gamee.Hiuk.Adapter;
using Gamee.Hiuk.Data;
using Gamee.Hiuk.FirebseAnalytic;
using Gamee.Hiuk.Game.Loader;
using Gamee.Hiuk.Test;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamee.Hiuk.Game 
{
    public class GameManagerNormal : GameManager
    {
        public override void NextLevelData()
        {
            GameData.LevelCurrent++;
            GameLoader.levelLoadData.Uplevel();
        }
        public override void BackLevelData()
        {
            if (GameData.LevelCurrent == 1) return;
            GameData.LevelCurrent--;
            GameLoader.UpdateLevelLoad(GameData.LevelCurrent);
            GameLoader.levelLoadData.DownLevel();
        }
        public async override void LoadLevelMap()
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
        public override void LogEventLevelReplay()
        {
            FirebaseAnalytic.LogLevelReplay(GameLoader.levelLoadData.LevelNameCurrent + GameData.LevelNameCurrent);
        }
        public override void LogEventLevelSkip()
        {
            FirebaseAnalytic.LogLevelSkip(GameLoader.levelLoadData.LevelNameCurrent + GameData.LevelNameCurrent);
        }
        public override void LogEventStartLevel()
        {
            var keyFirstStartLevel = "level_first_start" + GameLoader.levelLoadData.LevelNameCurrent;
            if (!PlayerPrefsAdapter.GetBool(keyFirstStartLevel))
            {
                FirebaseAnalytic.LogLevelFirstStart(GameLoader.levelLoadData.LevelNameCurrent + GameData.LevelNameCurrent);
                PlayerPrefsAdapter.SetBool(keyFirstStartLevel, true);
            }
            FirebaseAnalytic.LogLevelStart(GameLoader.levelLoadData.LevelNameCurrent + GameData.LevelNameCurrent);
        }
        public override void LogEventLevelWin()
        {
            FirebaseAnalytic.LogLevelCompleted(GameLoader.levelLoadData.LevelNameCurrent + GameData.LevelNameCurrent);
        }
        public override void LogEventLevelLose()
        {
            FirebaseAnalytic.LogLevelFailed(GameLoader.levelLoadData.LevelNameCurrent + GameData.LevelNameCurrent);
        }
    }
}

