using UnityEditor;
using UnityEngine;
namespace Gamee.Hiuk.Test
{
    public static class GameTest
    {
        public static string PathLevelAsset
        {
#if UNITY_EDITOR
            get => UnityEditor.EditorPrefs.GetString($"{Application.identifier}_leveldebug_pathlevel");
            set => UnityEditor.EditorPrefs.SetString($"{Application.identifier}_leveldebug_pathlevel", value);
#else
        get => "";
#endif
        }

        public static bool IsTest
        {
#if UNITY_EDITOR
            get => UnityEditor.EditorPrefs.GetBool($"{Application.identifier}_leveldebug_istest");
            set => UnityEditor.EditorPrefs.SetBool($"{Application.identifier}_leveldebug_istest", value);
#else
        get => false;
#endif
        }
        public static GameObject levelPrefab;

        public static GameObject LevelAsset
        {
            get
            {
                if (levelPrefab != null) return levelPrefab;

                if (string.IsNullOrEmpty(PathLevelAsset)) return null;
#if UNITY_EDITOR
                levelPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(PathLevelAsset);
#endif
                return levelPrefab;
            }
        }
    }
}

