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
        
    }

    IEnumerator LoadScene(int index)
    {
        //!TODO: 기본 전환 에니메이션 추가
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(index);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
     
}
