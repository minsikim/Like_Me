using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProfileController : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Text>().text = DataManager.Instance.Followers.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<Text>().text = DataManager.Instance.Followers.ToString();
    }
}
