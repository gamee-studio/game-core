#if UNITY_EDITOR
using Gamee.Hiuk.Test;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Gamee.HiuK.Editor
{
    [CustomEditor(typeof(LevelMap))]
    public class LevelMapEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (!serializedObject.isEditingMultipleObjects)
            {
                var level = (LevelMap) target;
                
                if (AssetDatabase.Contains(target))
                {
                    if (GUILayout.Button("Play"))
                    {
                        EditorSceneManager.OpenScene("Assets/_Root/Scenes/Loading.unity");
                        GameTest.IsTest = true;
                        GameTest.PathLevelAsset = AssetDatabase.GetAssetPath(level.gameObject);
                        EditorApplication.isPlaying = true;
                        //todo
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("Select a prefab level to play!");
                }
            }
        }

        static LevelMapEditor() { EditorApplication.playModeStateChanged += ModeChanged; }

        private static void ModeChanged(PlayModeStateChange obj)
        {
            if (obj == PlayModeStateChange.ExitingPlayMode)
            {
                GameTest.IsTest = false;
                GameTest.PathLevelAsset = "";
                GameTest.levelPrefab = null;
            }
        }
    }
}
#endif

