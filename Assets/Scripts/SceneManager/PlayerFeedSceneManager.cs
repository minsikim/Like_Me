using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerFeedSceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _userNameText;
    [SerializeField]
    private GameObject _profileImage;

    public ProfileData ProfileImages;

    void Start()
    {
        _userNameText.GetComponent<Text>().text = DataManager.Instance.UserName;
        _profileImage.GetComponent<Image>().sprite = ProfileImages.ProfileImages[DataManager.Instance.PhotoIndex].GetComponent<Image>().sprite;
    }

    void Update()
    {
        
    }
    
    public void ToGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
