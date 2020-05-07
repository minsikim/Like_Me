using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private const string USER_DATA_PATH = "/UserData.json";

    public int Followers = 0;
    public string UserName = "";

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadData();
    }

    public int AddFollowers(int likes)
    {
        int AddedFollowers = likes / 3;
        Followers = Followers + AddedFollowers;
        SaveData();

        return AddedFollowers;
    }
    private void LoadData()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + USER_DATA_PATH))
        {
            string _dataToString = System.IO.File.ReadAllText(Application.persistentDataPath + USER_DATA_PATH);
            SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(_dataToString);
            Followers = loadedObject.followers;
            UserName = loadedObject.userName;
        }
        else
        {
            Debug.Log("no file");
        }
        
    }

    private void SaveData()
    {
        SaveObject saveObject = new SaveObject
        {
            followers = Followers,
            userName = UserName,
        };
        string _jsonData = JsonUtility.ToJson(saveObject);
        System.IO.File.WriteAllText(Application.persistentDataPath + USER_DATA_PATH, _jsonData);
    }

    private class SaveObject
    {
        public int followers;
        public string userName;
    }

}

