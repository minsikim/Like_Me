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
    [SerializeField]
    private GameObject _followingText;
    [SerializeField]
    private GameObject _postText;

    public ProfileData ProfileImages;

    void Start()
    {
        _userNameText.GetComponent<Text>().text = DataManager.Instance.UserName;
        _profileImage.GetComponent<Image>().sprite = ProfileImages.ProfileImages[DataManager.Instance.PhotoIndex].GetComponent<Image>().sprite;
        _followingText.GetComponent<Text>().text = "" + (int)(DataManager.Instance.Followers * 0.8f);
        _postText.GetComponent<Text>().text = "" + DataManager.Instance.PostDatas.Count;
    }

    void Update()
    {
        
    }
    
    public void ToGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
