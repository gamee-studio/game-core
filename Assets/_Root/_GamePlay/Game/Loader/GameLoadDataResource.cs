using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamee.Hiuk.Adapter;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Gamee.Hiuk.Game.Loader
{
    [CreateAssetMenu(fileName = "GameLoadDataResource", menuName = "Scripttableobject/GameLoadDataResource", order = 1)]
    public class GameLoadDataResource : ScriptableObject
    {
        const string path = "GameLoadDataResource";
        private static GameLoadDataResource instance;
        public static GameLoadDataResource Instance => instance ??= Resources.Load<GameLoadDataResource>(path);

        [SerializeField] List<LevelLoadData> listLevelLoadData;
        [SerializeField] List<LevelInfo> listLevelInfo = new List<LevelInfo>();
        public List<LevelInfo> ListLevelInfo => listLevelInfo;
        public int LevelsCount => listLevelInfo.Count;
        public void Init()
        {
            foreach (var levelLoadData in listLevelLoadData)
            {
                levelLoadData.Init();
            }
        }
        public LevelLoadData GetLevelData(ELevelLoadType levelLoadType)
        {
            foreach (var levelLoadData in listLevelLoadData)
            {
                if (levelLoadData.Type.Equals(levelLoadType)) return levelLoadData;
            }
            return null;
        }
        public LevelInfo GetLevelInfo(int level) => listLevelInfo.FirstOrDefault(_ => _.LevelCurrent.Equals(level));
#region static api
        public static LevelLoadData GetLevelDataCurrent(ELevelLoadType levelLoadType)
        {
            return Instance.GetLevelData(levelLoadType);
        }
        public static void InitData()
        {
            Instance.Init();
        }
        public static int LevelCount => Instance.LevelsCount;
        public static List<LevelInfo> ListLevelInfoData => Instance.ListLevelInfo;
        public static LevelInfo GetLevelInfoData(int level) => Instance.GetLevelInfo(level);
#endregion
    }
#region editor
    [System.Serializable]
    public class LevelInfo
    {
        [SerializeField] internal int levelCurrent;
        [SerializeField] internal string levelRealName;
        [SerializeField] internal ELevelLoadType levelType;
        [SerializeField, ReadOnly] int levelReal;
        [SerializeField, ReadOnly] int indexLevelReal;
        public int LevelCurrent => levelCurrent;
        public int LevelReal => levelReal;
        public string LevelRealName => levelRealName;
        public int IndexLevelReal => indexLevelReal;
        public ELevelLoadType LevelType => levelType;
        public LevelInfo(int levelCurrent, int levelReal, string levelRealName, ELevelLoadType levelType, int indexLevelReal) 
        {
            this.levelCurrent = levelCurrent;
            this.levelReal = levelReal;
            this.levelRealName = levelRealName;
            this.levelType = levelType;
            this.indexLevelReal = indexLevelReal;
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(GameLoadDataResource), true)]
    [CanEditMultipleObjects]
    public class GameLoadDataResourceEditor : Editor
    {
        private GameLoadDataResource gameLoadResource;
        private List<LevelInfo> listLevelInfo = new List<LevelInfo>();
        private LevelLoadData levelNormal;
        private LevelLoadData levelPuzzle;
        private LevelLoadData levelGP2;

        private bool isLevelNormalLoop = false;
        private bool isLevelPuzzleLoop = false;
        private bool isLevelGP2Loop = false;
        private bool isUpdateCompleted => isLevelNormalLoop && isLevelPuzzleLoop && isLevelGP2Loop;
        protected void OnEnable()
        {
            gameLoadResource = (GameLoadDataResource)target;
            listLevelInfo = gameLoadResource.ListLevelInfo;

            levelNormal = gameLoadResource.GetLevelData(ELevelLoadType.LEVEL_NORMAL);
            levelPuzzle = gameLoadResource.GetLevelData(ELevelLoadType.LEVEL_PUZZLE);
            levelGP2 = gameLoadResource.GetLevelData(ELevelLoadType.LEVEL_GP2);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            if (EditorApplication.isPlaying) return;
            if (GUILayout.Button("Update", GUILayout.MinHeight(40), GUILayout.MinWidth(50)))
            {
                listLevelInfo.Clear();

                levelNormal.Index = 0;
                levelPuzzle.Index = 0;
                levelGP2.Index = 0;
                
                isLevelNormalLoop = false;
                isLevelPuzzleLoop = false;
                isLevelGP2Loop = false;

                int level = 0;
                int levelNormalCount = 0; 
                int levelPuzzleCount = 0; 
                int levelGP2Count = 0;

                ELevelLoadType type = ELevelLoadType.LEVEL_NORMAL;

                while (!isUpdateCompleted)
                {
                    type = ELevelLoadType.LEVEL_NORMAL;
                    level++;
                    if (level >= 5)
                    {
                        if (level % 5 == 0 && level >= GameConfig.LevelStartHavePuzzleCount) type = ELevelLoadType.LEVEL_PUZZLE;
                        if (level % 5 == 3 || level % 5 == 4) type = ELevelLoadType.LEVEL_GP2;
                    }
                    switch (type)
                    {
                        case ELevelLoadType.LEVEL_NORMAL:
                            levelNormalCount++;
                            if(levelNormalCount == levelNormal.LevelMax) 
                            {
                                if(isLevelPuzzleLoop && isLevelGP2Loop) 
                                {
                                    isLevelNormalLoop = true;
                                    listLevelInfo.Add(new LevelInfo(level, levelNormalCount, string.Format(levelNormal.PathLevel, levelNormalCount), levelNormal.Type, levelNormalCount - 1));
                                    break;
                                }
                            }
                            if (levelNormalCount > levelNormal.LevelMax) 
                            {
                                isLevelNormalLoop = true;
                                if (isUpdateCompleted) break;
                                levelNormalCount = 1;
                            }
                            listLevelInfo.Add(new LevelInfo(level, levelNormalCount, string.Format(levelNormal.PathLevel, levelNormalCount), levelNormal.Type, levelNormalCount -1));
                            break;
                        case ELevelLoadType.LEVEL_PUZZLE:
                            levelPuzzleCount++;
                            if (levelPuzzleCount == levelPuzzle.LevelMax)
                            {
                                if (isLevelNormalLoop && isLevelGP2Loop)
                                {
                                    isLevelPuzzleLoop = true;
                                    listLevelInfo.Add(new LevelInfo(level, levelPuzzleCount, string.Format(levelPuzzle.PathLevel, levelPuzzleCount), levelPuzzle.Type, levelPuzzleCount - 1));
                                    break;
                                }
                            }
                            if (levelPuzzleCount > levelPuzzle.LevelMax)
                            {
                                isLevelPuzzleLoop = true;
                                if (isUpdateCompleted) break;
                                levelPuzzleCount = 1;
                            }
                            listLevelInfo.Add(new LevelInfo(level, levelPuzzleCount, string.Format(levelPuzzle.PathLevel, levelPuzzleCount), levelPuzzle.Type, levelPuzzleCount -1));
                            break;
                        case ELevelLoadType.LEVEL_GP2:
                            levelGP2Count++;
                            if (levelGP2Count == levelGP2.LevelMax)
                            {
                                if (isLevelNormalLoop && isLevelNormalLoop)
                                {
                                    isLevelGP2Loop = true;
                                    listLevelInfo.Add(new LevelInfo(level, levelGP2Count, string.Format(levelGP2.PathLevel, levelGP2Count), levelGP2.Type, levelGP2Count - 1));
                                    break;
                                }
                            }
                            if (levelGP2Count > levelGP2.LevelMax)
                            {
                                isLevelGP2Loop = true;
                                if (isUpdateCompleted) break;
                                levelGP2Count = 1;
                            }
                            listLevelInfo.Add(new LevelInfo(level, levelGP2Count, string.Format(levelGP2.PathLevel, levelGP2Count), levelGP2.Type, levelGP2Count - 1));
                            break;
                    }

                    if (level > 1000) 
                    {
                        isLevelNormalLoop = true;
                        isLevelPuzzleLoop = true;
                        isLevelGP2Loop = true;
                        break;
                    }
                }
                AssetDatabase.SaveAssets();
                EditorUtility.SetDirty(gameLoadResource);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
#endregion
}
