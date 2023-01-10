using System;
using UnityEngine;
namespace Gamee.Hiuk.Level 
{
    public abstract class LevelMap : MonoBehaviour, ILevel
    {
        public Action ActionWin;
        public Action ActionLose;

        public abstract void Init();
        public void Lose()
        {
            ActionLose?.Invoke();
        }

        public void Win()
        {
            ActionWin?.Invoke();
        }
    }
}

