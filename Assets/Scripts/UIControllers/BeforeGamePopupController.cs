using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeforeGamePopupController : MonoBehaviour
{

    public Text currentLevelText;
    public Text targetLevelText;

    public StageData stageData;

    private Level _currentLevel;
    private Level _currentLevelProgression;

    void Start()
    {
        _currentLevel = DataManager.Instance.CurrentLevel;

        GetComponent<CanvasGroup>().alpha = 0;
    }

    void Update()
    {
        if(GetComponent<CanvasGroup>().alpha < 1)
            GetComponent<CanvasGroup>().alpha += Time.deltaTime;
    }
    public void Confirm()
    {
        PopupCanvasManager.Instance.ClosePopup(gameObject);
        GameInstance.Instance.StartGame();
    }

    public void Close()
    {
        SceneManager.LoadScene("PlayerFeed");
        PopupCanvasManager.Instance.ClosePopup(gameObject);
    }

    public void ToFeedScene()
    {
        SceneManager.LoadScene("PlayerFeed");
    }

    private void UpdateCurrentProgression()
    {
        //currentLevelText.text = 
    }

}
