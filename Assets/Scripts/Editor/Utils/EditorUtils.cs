using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;

public class EditorUtils
{
    public static List<T> GetAssetsWithScript<T>(string path) where T : MonoBehaviour
    {
        T tmp;
        string assetPath;
        GameObject asset;

        List<T> assetList = new List<T>();
        string[] guids = AssetDatabase.FindAssets("t:Prefab", new string[] { path });
        for (int i = 0; i < guids.Length; ++i)
        {
            assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            asset = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject;
            tmp = asset.GetComponent<T>();
            if (tmp != null)
            {
                assetList.Add(tmp);
            }
        }
        return assetList;
    }

    public static List<T> GetAssetsOfType<T>(string folderPath, string filter) where T : ScriptableObject
    {
        T tmp;
        string assetPath;

        List<T> assetList = new List<T>();
        string[] guids = AssetDatabase.FindAssets(filter, new string[] { folderPath });
        for (int i = 0; i < guids.Length; ++i)
        {
            assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            tmp = AssetDatabase.LoadAssetAtPath(assetPath, typeof(T)) as T;
            if (tmp != null)
            {
                assetList.Add(tmp);
            }
        }
        return assetList;
    }

    public static List<T> GetListFromEnum<T>()
    {
        List<T> result = new List<T>();
        System.Array enums = System.Enum.GetValues(typeof(T));
        foreach (T e in enums)
        {
            result.Add(e);
        }
        return result;
    }

}
