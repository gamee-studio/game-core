#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class MenuEditor
{
    private const string LOADING_SCENE = "Loading";
    private const string GAME_SCENE = "GamePlay";


    private static void ChangeScene(string name)
    {
        EditorSceneManager.SaveOpenScenes();
        EditorSceneManager.OpenScene(Application.dataPath + "/_Root/Scenes/" + name + ".unity");
    }


    [MenuItem("Scenes/Open Loading Scene", false, 33)]
    private static void OpenLoadingScene()
    {
        ChangeScene(LOADING_SCENE);
    }

    [MenuItem("Scenes/Open Loading Scene", true, 33)]
    private static bool CanOpenLoadingScene()
    {
        return (EditorSceneManager.GetActiveScene().name != LOADING_SCENE);
    }

    [MenuItem("Scenes/Open Game Scene", false, 33)]
    private static void OpenGameScene()
    {
        ChangeScene(GAME_SCENE);
    }

    [MenuItem("Scenes/Open Game Scene", true, 33)]
    private static bool CanOpenGameScene()
    {
        return (EditorSceneManager.GetActiveScene().name != GAME_SCENE);
    }

    [MenuItem("Scenes/Play Loading Scene", false, 44)]
    private static void PlayLauncherScene()
    {
        EditorSceneManager.SaveOpenScenes();
        if (EditorSceneManager.GetActiveScene().name != LOADING_SCENE) ChangeScene(LOADING_SCENE);
        EditorApplication.isPlaying = true;
    }

    [MenuItem("Scenes/Play Loading Scene", true, 44)]
    private static bool CanPlayLauncherScene()
    {
        return !Application.isPlaying;
    }

}
#endif