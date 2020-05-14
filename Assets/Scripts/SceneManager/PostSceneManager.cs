using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PostSceneManager : MonoBehaviour
{
    public GameObject UserName;
    public GameObject UserImage;
    public GameObject PostImage;
    public GameObject PostLikes;
    public GameObject PostDate;

    public ProfileData profileData;
    public ImageListData postImages;

    private PostData _currentPost;
    private bool _updateTime = false;

    private void Start()
    {
        UserName.GetComponent<Text>().text = DataManager.Instance.UserName;
        UserImage.GetComponent<Image>().sprite = profileData.ProfileImages[DataManager.Instance.PhotoIndex].GetComponent<Image>().sprite;

        if (!DataManager.Instance.CurrentPost.Equals(default(PostData)))
        {
            _currentPost = DataManager.Instance.CurrentPost;
            Debug.Log("new post is assigned" + _currentPost.id);
        }

        else return;

        UpdatePostInfo();
    }
    private void Update()
    {
        if(_updateTime)
            PostDate.GetComponent<Text>().text = GetTimeFromNow(_currentPost.PostTime);
    }
    public void UpdatePostInfo()
    {
        PostImage.GetComponent<Image>().sprite = postImages.Images[_currentPost.SpriteIndex];
        PostLikes.GetComponent<Text>().text = _currentPost.Likes + " Likes";
        PostDate.GetComponent<Text>().text = GetTimeFromNow(_currentPost.PostTime);
    }

    public string GetTimeFromNow(string tString)
    {
        return GetTimeFromNow(Convert.ToDateTime(tString));
    }
    public string GetTimeFromNow(DateTime t)
    {
        TimeSpan ts = DateTime.Now - t;
        if (ts < new TimeSpan(30, 0, 0, 0, 0))
        {
            if (ts < new TimeSpan(7, 0, 0, 0, 0))
            {
                if (ts < new TimeSpan(1, 0, 0, 0, 0))
                {
                    if (ts < new TimeSpan(0, 1, 0, 0, 0))
                    {
                        if (ts < new TimeSpan(0, 0, 1, 0, 0))
                        {
                            _updateTime = true;
                            return ts.Seconds + " Seconds ago";
                        }
                        else
                        {
                            _updateTime = false;
                            return ts.Minutes + " Minutes ago";
                        }
                    }
                    else
                    {
                        _updateTime = false;
                        return ts.Hours + " Hours ago";
                    }
                }
                else
                {
                    _updateTime = false;
                    return ts.Days + " Days ago";
                }
            }
            else
            {
                _updateTime = false;
                return ts.Days / 7 + " Weeks ago";
            }
        }
        else
        {
            _updateTime = false;
            return ts.Days / 365 + " Months ago";
        }
    }

    public void ToGameScene()
    {
        SceneManager.LoadScene("Game");
    }
    public void ToFeedScene()
    {
        SceneManager.LoadScene("PlayerFeed");
    }
}
