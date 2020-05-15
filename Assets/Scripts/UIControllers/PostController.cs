using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostController : MonoBehaviour
{
    public PostData postData;

    public GameObject BestTag;

    public bool best;

    private void Start()
    {
        if (best)
        {
            BestTag.SetActive(true);
        }
        else
        {
            BestTag.SetActive(false);
        }
    }

    public void OnLoadPostScene()
    {
        DataManager.Instance.CurrentPost = postData;
        SceneManager.LoadScene("Post");
    }
}
