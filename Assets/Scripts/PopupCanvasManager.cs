﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCanvasManager : MonoBehaviour
{
    private static PopupCanvasManager _instance;

    public static PopupCanvasManager Instance;

    public GameObject BackPanel;
    public GameObject Canvas;
    public GameObject BeforeGamePopupPrefab;
    public GameObject AfterGamePopupPrefab;

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
    }

    public void OpenPopup(GameObject popupPrefab)
    {
        BackPanel.SetActive(true);

        GameObject p = Instantiate(popupPrefab, Canvas.transform);
        p.transform.SetParent(Canvas.transform);
    }

    public void ClosePopup(GameObject popupGameObject)
    {
        BackPanel.SetActive(false);
        Destroy(popupGameObject);
    }

    

    //IEnumerator ClosePopup()
    //{
    //    Color c = BackPanel.GetComponent<Image>().color;
    //    while(BackPanel.GetComponent<Image>().color.a > 0)
    //    {
    //        c = new Color(c.r, c.g, c.b, c.a - Time.deltaTime);
    //        BackPanel.GetComponent<Image>().color = c;
    //    }
    //    yield return new WaitForSeconds(2f);
    //    BackPanel.SetActive(false);
    //}
}
