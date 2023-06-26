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
    [SerializeField] int winChallengePercent = 85;
    [SerializeField] int watchVideoValue = 5;
    [SerializeField] string linkFB;
    [SerializeField] int levelShowBtnSkip = 4;
    [SerializeField] int levelShowCutSceneCount = 5;
    [SerializeField] int levelChallengeUnlock = 30;
    [SerializeField] int levelPlayToUnlockCount = 20;
    [SerializeField] int levelChallengeBonusCount = 5;
    [SerializeField] int levelChallengeMaxCount = 15;
    [SerializeField] int timeCheckLevelChallenge = 3;
    [SerializeField] int levelStartHavePuzzleCount = 10;
    [SerializeField] int levelDrawUnlock = 30;
    [SerializeField] int levelPlayToUnlockDrawCount = 20;
    [SerializeField] int levelDrawMaxCount = 15;
    [SerializeField] int levelDrawBonusCount = 5;
    [SerializeField] float levelDrawPlayerMoveTime = 3;

    [Header("Draw Mode")]
    [SerializeField, Range(0, 5)] float timeDelayDrawWin = 1;
    [SerializeField, Range(0, 5)] float timeDelayDrawLose = 1;

    #region static api
    public static bool IsShowDebug => Instance.isShowDebug;
    public static int LevelShowUpdateCount => Instance.levelShowUpdateCount;
    public static int LevelShowRateCount => Instance.levelShowRateCount;
    public static int LevelShowRateNextValue => Instance.levelShowRateNextValue;
    public static int WinPercent => Instance.winPercent;
    public static int WinChallengePercent => Instance.winChallengePercent;
    public static int WatchVideoValue => Instance.watchVideoValue;
    public static string LinkFB => Instance.linkFB;
    public static int LevelShowBtnSkip => Instance.levelShowBtnSkip;
    public static int LevelShowCutSceneCount => Instance.levelShowCutSceneCount;
    public static int LevelChallengeUnlock => Instance.levelChallengeUnlock;
    public static int LevelPlayToUnlockCount => Instance.levelPlayToUnlockCount;
    public static int LevelChallengeBonusCount => Instance.levelChallengeBonusCount;
    public static int LevelChallengeMaxCount => Instance.levelChallengeMaxCount;
    public static int TimeCheckLevelChallenge => Instance.timeCheckLevelChallenge;
    public static int LevelStartHavePuzzleCount => Instance.levelStartHavePuzzleCount;
    public static int LevelDrawUnlock => Instance.levelDrawUnlock;
    public static int LevelPlayToUnlockDrawCount => Instance.levelPlayToUnlockDrawCount;
    public static int LevelDrawMaxCount => Instance.levelDrawMaxCount;
    public static int LevelDrawBonusCount => Instance.levelDrawBonusCount;
    public static float TimeDelayDrawWin => Instance.timeDelayDrawWin;
    public static float TimeDelayDrawLose => Instance.timeDelayDrawLose;
    public static float LevelDrawPlayerMoveTime => Instance.levelDrawPlayerMoveTime;
    #endregion
}

