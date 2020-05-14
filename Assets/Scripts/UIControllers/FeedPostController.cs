using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedPostController : MonoBehaviour
{

    private const float POST_HEIGHT = 295f;
    private const float POST_WIDTH = 295f;


    void Start()
    {
        //포스트 출력
        UpdatePostPositions();
    }
    //!TODO: 작업중
    public void UpdatePostPositions()
    {
        List<PostData> _posts = DataManager.Instance.PostDatas;
        _posts.Reverse();
        for (int i = 0; i < _posts.Count; i++)
        {
            PostData p = _posts[i];
            if(i % 3 == 0)
            {
                
            }
            else if (i % 3 == 1)
            {

            }
            else if (i % 3 == 2)
            {

            }
        }
    }

}
