using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeforeGamePopupController : MonoBehaviour
{



    void Start()
    {
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

}
