using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstance : MonoBehaviour
{
    private static GameInstance _instance;
    public static GameInstance Instance { get { return _instance; } }

    public AudioManager _audioManager;

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
        Invoke("StartGame", 2.0f);
#else
        Invoke("StartGame", 2.0f);
#endif
    }

    public void StartGame()
    {
        SceneManager.LoadScene("FirstPlay");
        Debug.Log("StartGame");
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
     
}
