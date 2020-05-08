using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

[CustomEditor(typeof(DataManager))]
public class DataManger_Editor : Editor
{

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Delete Local Data", GUILayout.Height(32f)))
        {
            DeleteData();
        }
    }
    
    public void DeleteData()
    {
        Debug.LogError("Data Deleted!");
    }

    private void OnEnable()
    {

    }
    private void OnDisable() {
        
    }
}