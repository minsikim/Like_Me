using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostController : MonoBehaviour
{
    public PostData postData;
    
    public void OnLoadPostScene()
    {
        DataManager.Instance.CurrentPost = postData;
        SceneManager.LoadScene("Post");
    }
}
