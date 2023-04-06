using UnityEngine;
public class GameConfig : ScriptableObject
{
    const string path = "GameConfig";
    private static GameConfig instance;
    public static GameConfig Instance => instance ??= Resources.Load<GameConfig>(path);

    [SerializeField] bool isShowDebug = false;
    [SerializeField] int levelShowUpdateCount = 10;
    [SerializeField] int levelShowRateCount = 20;
    [SerializeField] int levelShowRateNextValue = 20;
    [SerializeField] int winPercent = 75;
    [SerializeField] int watchVideoValue = 5;
    [SerializeField] string linkFB;
    [SerializeField] int levelShowBtnSkip = 4;
    [SerializeField] int levelShowCutSceneCount = 5;
     
    #region static api
    public static bool IsShowDebug => Instance.isShowDebug;
    public static int LevelShowUpdateCount => Instance.levelShowUpdateCount;
    public static int LevelShowRateCount => Instance.levelShowRateCount;
    public static int LevelShowRateNextValue => Instance.levelShowRateNextValue;
    public static int WinPercent => Instance.winPercent;
    public static int WatchVideoValue => Instance.watchVideoValue;
    public static string LinkFB => Instance.linkFB;
    public static int LevelShowBtnSkip => Instance.levelShowBtnSkip;
    public static int LevelShowCutSceneCount => Instance.levelShowCutSceneCount;
    #endregion
}

