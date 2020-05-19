using UnityEngine;
using UnityEditor;

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
        System.IO.File.Delete(Application.persistentDataPath + DataManager.USER_DATA_PATH);
        Debug.LogError("Data Deleted!");
    }

    private void OnEnable()
    {

    }
    private void OnDisable() {
        
    }
}