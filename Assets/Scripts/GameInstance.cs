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

        _audioManager = Object.FindObjectOfType<AudioManager>();
    }
    private void Start()
    {
        Invoke("StartGame", 2.0f);
    }

    public void StartGame()
    {
        if (DataManager.Instance.Loaded)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
     
}
