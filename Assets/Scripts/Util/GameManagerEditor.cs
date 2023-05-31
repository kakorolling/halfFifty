using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        DirectoryInfo di = new DirectoryInfo(Application.persistentDataPath);
        var files = Array.FindAll(di.GetFiles(), e => e.Name != ".DS_Store");
        if (GUILayout.Button("DELETE ALL DATA"))
        {
            foreach (FileInfo file in files)
            {
                file.Delete();
            }
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        foreach (FileInfo file in files)
        {
            GUILayout.BeginHorizontal();
            GUILayout.TextArea(file.Name);
            if (GUILayout.Button("Delete")) file.Delete();
            GUILayout.EndHorizontal();
        }
    }
}
