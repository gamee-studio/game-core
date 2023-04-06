using Gamee.Hiuk.Game.Loader;
using Gamee.Hiuk.Level.Player;
using System;
using System.Collections;
using UnityEngine;
namespace Gamee.Hiuk.Level 
{
    public abstract class LevelMap : MonoBehaviour, ILevel
    {
        [SerializeField] protected ELevelState state;
        public Action ActionWin;
        public Action ActionLose;

        protected Coroutine coroutineLevelWin;
        protected Coroutine coroutineLevelLose;

        protected  PlayerController player;
        protected LevelLoadData levelData;
        public PlayerController Player => player;
        public LevelLoadData LevelData => levelData;
        public virtual void Init() 
        {
            player = this.GetComponentInChildren<PlayerController>();
            levelData = GameLoader.levelLoadData;
        }
        public virtual void Lose(Action actionCompleted = null)
        {
            if (state == ELevelState.LEVEL_WIN) return;
            state = ELevelState.LEVEL_LOSING;
            if(coroutineLevelWin != null) StopCoroutine(coroutineLevelWin);
            coroutineLevelLose = StartCoroutine(DelayTime(levelData.TimeDelayLose, () =>
            {
                state = ELevelState.LEVEL_LOSE;
                ActionLose?.Invoke();
                actionCompleted?.Invoke();
            }));
        }

        public virtual void Win(Action actionCompleted = null)
        {
            if (state == ELevelState.LEVEL_LOSING) return;
            state = ELevelState.LEVEL_WINNING;
            coroutineLevelWin = StartCoroutine(DelayTime(levelData.TimeDelayWin, () =>
            {
                state = ELevelState.LEVEL_WIN;
                if (coroutineLevelLose != null) StopCoroutine(coroutineLevelLose);
                ActionWin?.Invoke();
                actionCompleted?.Invoke();
            }));
        }
        public virtual void Clear() 
        {
            if(coroutineLevelLose != null) StopCoroutine(coroutineLevelLose);
            if(coroutineLevelWin != null) StopCoroutine(coroutineLevelWin);
        }

        public IEnumerator DelayTime(float time = 0.5f, Action actionCompleted = null) 
        {
            yield return new WaitForSeconds(time);
            actionCompleted?.Invoke();
        }
        #region draw bound screen
        void OnDrawGizmos()
        {
            float verticalHeightSeen = Camera.main.orthographicSize * 2.0f;
            float verticalWidthSeen = verticalHeightSeen * Camera.main.aspect;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, new Vector3(verticalWidthSeen, verticalHeightSeen, 0));
        }
        #endregion
    }
}

