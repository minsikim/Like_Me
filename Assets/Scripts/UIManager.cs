using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _likeText;

    [SerializeField]
    private GameObject _gameoverCanvas;

    [SerializeField]
    private Text _gameoverText;

    void Start()
    {

    }

    void Update()
    {
        UpdateLikeText();
    }

    public void UpdateLikeText()
    {
        _likeText.text = GameManager.Likes + " Likes";
    }
    public void GameoverEnable()
    {
        _gameoverText.text = GameManager.Likes + " Likes";
        _gameoverCanvas.SetActive(true);
    }
    public void GameoverDisable()
    {
        _gameoverText.text = "0 Likes";
        _gameoverCanvas.SetActive(false);
    }
}
