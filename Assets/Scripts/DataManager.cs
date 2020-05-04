using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    public int Followers;

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

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public int AddFollowers(int likes)
    {
        Debug.Log("Likes: " + likes);
        Debug.Log("Add Follower: " + likes / 3);
        Followers = Followers + likes / 3;
        return Followers;
    }

}
