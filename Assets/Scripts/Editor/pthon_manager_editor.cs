using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(python_manager))]
public class pthon_manager_editor : Editor
{
    python_manager targetManager;

    private void OnEnable()
    {
        targetManager = target as python_manager;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Launch Python Script", GUILayout.Height(35)))
        {
            Debug.LogError("Launch Python Script");
        }
    }




}
