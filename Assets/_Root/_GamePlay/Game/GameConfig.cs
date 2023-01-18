using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameConfig : ScriptableObject
{
    const string path = "GameConfig";
    private static GameConfig instance;
    public static GameConfig Instance => instance ??= Resources.Load<GameConfig>(path);
    [SerializeField] int levelShowUpdateCount = 10;
    #region static api
    public static int LevelShowUpdateCount => Instance.levelShowUpdateCount;
    #endregion
}

