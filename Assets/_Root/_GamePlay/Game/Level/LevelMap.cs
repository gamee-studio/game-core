using Gamee.Hiuk.Game.Loader;
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
        public abstract void Init();
        public virtual void Lose()
        {
            if (state == ELevelState.LEVEL_WIN) return;
            state = ELevelState.LEVEL_LOSING;
            if(coroutineLevelWin != null) StopCoroutine(coroutineLevelWin);
            coroutineLevelLose = StartCoroutine(DelayTime(GameLoader.levelLoadData.TimeDelayLose, () =>
            {
                state = ELevelState.LEVEL_LOSE;
                ActionLose?.Invoke();
            }));
        }

        public virtual void Win()
        {
            if (state == ELevelState.LEVEL_LOSING) return;
            state = ELevelState.LEVEL_WINNING;
            coroutineLevelWin = StartCoroutine(DelayTime(GameLoader.levelLoadData.TimeDelayWin, () =>
            {
                state = ELevelState.LEVEL_WIN;
                if (coroutineLevelLose != null) StopCoroutine(coroutineLevelLose);
                ActionWin?.Invoke();
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

