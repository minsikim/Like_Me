﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;
    public static DataManager Instance { get { return _instance; } }

    public const string USER_DATA_PATH = "/UserData.json";

    public bool Loaded = false;

    private Guid id;

    public string UserName = "";
    public int PhotoIndex = 0;
    public Level CurrentLevel = Level.Follower1;

    public int Followers = 0;

    public List<PostData> PostDatas = new List<PostData>();

    public PostData CurrentPost;

    public PostData LastPostData;

    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
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

    private void LoadData()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + USER_DATA_PATH))
        {
            string _dataToString = System.IO.File.ReadAllText(Application.persistentDataPath + USER_DATA_PATH);
            SaveObject loadedObject = JsonUtility.FromJson<SaveObject>(_dataToString);

            Followers       = loadedObject.followers;
            UserName        = loadedObject.userName;
            id              = loadedObject.id;
            PhotoIndex      = loadedObject.photoIndex;
            CurrentLevel    = loadedObject.currentLevel;
            if(loadedObject.postDatas != null)
            {
                PostDatas = loadedObject.postDatas;
                CurrentPost = PostDatas[0];
            }
                

            CalculateLevel(Followers);

            Loaded = true;
            Debug.Log("Loaded:" + _dataToString);
        }
        else
        {
            Loaded = false;
            Debug.Log("NO Data file in " + Application.persistentDataPath + USER_DATA_PATH);
        }
        
    }

    public void SaveData()
    {
        SaveObject saveObject = new SaveObject
        {
            followers = Followers,
            userName = UserName,
            id = id,
            photoIndex = PhotoIndex,
            currentLevel = CurrentLevel,
            postDatas = PostDatas
        };
        string _jsonData = JsonUtility.ToJson(saveObject);
        System.IO.File.WriteAllText(Application.persistentDataPath + USER_DATA_PATH, _jsonData);

        Debug.Log("Saved:" + _jsonData);
    }

    public void NewId()
    {
        id = Guid.NewGuid();
    }

    public int AddFollowers(int likes)
    {
        int AddedFollowers = CalcAddedFollowers(likes);
        Followers = Followers + AddedFollowers;
        CalculateLevel(Followers);
        SaveData();

        return AddedFollowers;
    }
    public int CalcAddedFollowers(int likes)
    {
        int AddedFollowers = likes / 3;
        return AddedFollowers;
    }


        private void CalculateLevel(int Followers)
    {
        Level currentLevel;

        switch (Followers)
        {
            case int n when (n < 10):
                currentLevel = Level.Follower1;
                break;
            case int n when (n >= 10 && n < 100):
                currentLevel = Level.Follower10;
                break;
            case int n when (n >= 100 && n < 1000):
                currentLevel = Level.Follower100;
                break;
            case int n when (n < 10000):
                currentLevel = Level.Follower1K;
                break;
            case int n when (n >= 10000 && n < 100000):
                currentLevel = Level.Follower10K;
                break;
            case int n when (n >= 100000 && n < 1000000):
                currentLevel = Level.Follower100K;
                break;
            case int n when (n < 10000000):
                currentLevel = Level.Follower1M;
                break;
            case int n when (n >= 10000000 && n < 100000000):
                currentLevel = Level.Follower10M;
                break;
            case int n when (n >= 100000000 && n < 1000000000):
                currentLevel = Level.Follower100M;
                break;
            case int n when (n >= 100000000):
                currentLevel = Level.Follower1B;
                break;
            default:
                currentLevel = Level.Follower1;
                break;
        }
        CurrentLevel = currentLevel;
    }

    public int GetBestLikes()
    {
        return PostDatas[GetBestPostIndex()].Likes;
    }

    private int GetBestPostIndex()
    {
        int bestIndex = 0;
        int currentBest = 0;
        for (int i = 0; i < PostDatas.Count; i++)
        {
            if (currentBest < PostDatas[i].Likes)
            {
                currentBest = PostDatas[i].Likes;
                bestIndex = i;
            }

        }
        return bestIndex;
    }

    private class SaveObject
    {
        public int followers;
        public string userName;
        public Guid id;
        public int photoIndex;
        public Level currentLevel;
        public List<PostData> postDatas;
    }

}