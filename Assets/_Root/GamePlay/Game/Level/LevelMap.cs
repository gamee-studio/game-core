using System;
using UnityEngine;

public abstract class LevelMap : MonoBehaviour, ILevel
{
    public Action<LevelMap> ActionWin;
    public Action<LevelMap> ActionLose;

    public abstract void Init();
    public void Lose()
    {
        ActionLose?.Invoke(this);
    }

    public void Win()
    {
        ActionWin?.Invoke(this);
    }
}
