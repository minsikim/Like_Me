using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour
{
    private static GameInstance _instance;
    public static GameInstance Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Application.targetFrameRate = 60;
    }
    private void Start()
    {
#if UNITY_EDITOR
        if(SceneManager.GetActiveScene().name == "_Preload")
        {
            Invoke("ToGameScene", 2.0f);
        }
#else
        Invoke("ToGameScene", 2.0f);
#endif
    }

    public void ToGameScene()
    {
        SceneManager.LoadScene("FirstPlay");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public event Action onStartGame;
    public void StartGame()
    {
        if(onStartGame != null)
        {
            onStartGame();
        }
    }
    public event Action onGameOver;
    public void GameOver()
    {
        if (onGameOver != null)
        {
            onGameOver();
        }
    }
}
