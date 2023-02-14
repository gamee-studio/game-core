using UnityEngine;
using System;
using System.Collections.Generic;
using System.IO;

public class SceneProperty: PropertyAttribute 
{
    private string[] items = new string[] { "<None>" };

    public int index = 0;
    public string[] Item
    {
        set { items = value; }
        get { return items; }
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomPropertyDrawer(typeof(SceneProperty))]
public class ScenePropertyDrawer : UnityEditor.PropertyDrawer
{
    SceneProperty custom { get { return (SceneProperty)attribute; } }

    public override void OnGUI(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
    {
        UnityEditor.EditorGUI.BeginChangeCheck();

        List<string> scenes = new List<string>();
        foreach (var scene in UnityEditor.EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                var name = Path.GetFileNameWithoutExtension(scene.path);
                scenes.Add(name);
            }
        }

        if (scenes.Count == 0) return;
        custom.Item = new string[scenes.Count];
        for (int i = 0; i < scenes.Count; i++)
        {
            custom.Item[i] = scenes[i];
        }
        custom.index = Array.IndexOf(custom.Item, property.stringValue);
        custom.index = UnityEditor.EditorGUI.Popup(position, label.text, custom.index, custom.Item);

        if (UnityEditor.EditorGUI.EndChangeCheck())
        {
            property.stringValue = custom.Item[custom.index];
        }
    }
}
#endif