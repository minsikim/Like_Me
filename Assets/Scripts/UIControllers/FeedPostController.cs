using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedPostController : MonoBehaviour
{

    public GameObject PostPrefab;
    public ImageListData PostImageList;

    public GameObject NoPostCanvas;

    private const float POST_HEIGHT = 295f;
    private const float POST_WIDTH = 295f;
    private const float POST_AREA_HEIGHT = 300f;
    private const float POST_AREA_WIDTH = 300f;
    private const float TOP_POSITION_Y = -147.5f;

    private List<GameObject> _posts = new List<GameObject>();

    void Start()
    {
        //포스트 출력
        NoPostCanvas.SetActive(false);
        UpdatePosts();
    }

    public void UpdatePosts()
    {
        List<PostData> _postDatas = new List<PostData>(DataManager.Instance.PostDatas);
        
        if (_postDatas == null)
        {
            Debug.LogError("post data is null");
            return;
        }
        else if(_postDatas.Count == 0)
        {
            EnableNoPosts();
            return;
        }

        _postDatas.Reverse();

        int _bestIndex = GetBestPostIndex(_postDatas);

        for (int i = 0; i < _postDatas.Count; i++)
        {
            GameObject p = Instantiate(PostPrefab, transform);
            p.transform.SetParent(transform);
            p.GetComponent<PostController>().postData = _postDatas[i];
            p.GetComponent<Image>().sprite = PostImageList.Images[_postDatas[i].SpriteIndex];

            if(_bestIndex == i)
            {
                p.GetComponent<PostController>().best = true;
            }

            _posts.Add(p);
        }

        UpdatePostPositions();
    }

    private int GetBestPostIndex(List<PostData> postDatas)
    {
        int bestIndex = 0;
        int currentBest = 0;
        for (int i = 0; i < postDatas.Count; i++)
        {
            if (currentBest < postDatas[i].Likes)
            {
                currentBest = postDatas[i].Likes;
                bestIndex = i;
            }
                
        }
        return bestIndex;
    }

    public void UpdatePostPositions()
    {
        for (int i = 0; i < _posts.Count; i++)
        {
            GameObject p = _posts[i];
            if(i % 3 == 0)
            {
                p.GetComponent<RectTransform>().anchoredPosition = new Vector2(-POST_AREA_WIDTH, TOP_POSITION_Y - (i / 3 * POST_AREA_HEIGHT));
            }
            else if (i % 3 == 1)
            {
                p.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, TOP_POSITION_Y - (i / 3 * POST_AREA_HEIGHT));
            }
            else if (i % 3 == 2)
            {
                p.GetComponent<RectTransform>().anchoredPosition = new Vector2(POST_AREA_WIDTH, TOP_POSITION_Y - (i / 3 * POST_AREA_HEIGHT));
            }
            Debug.Log("Positioned Post: " + p.GetComponent<RectTransform>().anchoredPosition);
        }
        
    }

    public void EnableNoPosts()
    {
        NoPostCanvas.SetActive(true);
    }
}
