using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Bounds
{
    public static float xMax { get; set; }
    public static float xMin { get; set; }
    public static float yMax { get; set; }
    public static float yMin { get; set; }

}



public class GameManager : MonoBehaviour
{
    public static int Likes;
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _spawnManager;
    [SerializeField]
    private GameObject _tutorialCanvas;
    [SerializeField]
    private GameObject _uiManager;

    public enum Level // your custom enumeration
    {
        Like100,
        Like300,
        Like500,
        Like1000,
        Like5000,
        Like10000
    };

    public Level currentLevel;

    void Start()
    {
        Bounds.xMin = -3.8f;
        Bounds.xMax =  3.6f;
        Bounds.yMin = -1.8f;
        Bounds.yMax =  6.3f;
        currentLevel = Level.Like100;
        _uiManager.GetComponent<UIManager>().GameoverDisable();
    }

    void Update()
    {
        if ((Input.GetKeyDown("space") || Input.GetMouseButton(0)) && (_player.GetComponent<Player>()._isDead || !_player.activeInHierarchy) )
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        _uiManager.GetComponent<UIManager>().GameoverDisable();
        //Disable Tutorial Canvas
        _tutorialCanvas.SetActive(false);
        Likes = 0;
        //Enable SpawnManager
        _player.SetActive(true);
        _player.GetComponent<Player>()._isDead = false;
        _player.GetComponent<Player>()._isDisoriented = false;
        //Enable Player
        _spawnManager.SetActive(true);
        _spawnManager.GetComponent<SpawnManager>().CallSpawn();
    }
    public void OnGameOver()
    {
        //Disable Tutorial Canvas
        _tutorialCanvas.SetActive(true);
        //Enable SpawnManager
        _player.SetActive(false);
        //Enable Player
        _spawnManager.SetActive(false);
        _uiManager.GetComponent<UIManager>().GameoverEnable();
    }
    public static void AddLikes(int amount)
    {
        Likes += amount;
        Debug.Log("Current Likes: " + Likes);
    }
}
