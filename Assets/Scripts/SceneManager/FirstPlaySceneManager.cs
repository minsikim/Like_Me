using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstPlaySceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _userInput;
    [SerializeField]
    private GameObject _userProfiles;

    private int _userProfileIndex;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectProfile(int index)
    {
        _userProfiles.transform.GetChild(index).GetChild(0).gameObject.SetActive(true);
        if (index == 0) _userProfiles.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        if (index == 1) _userProfiles.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        _userProfileIndex = index;
    }

    public void ConfirmProfile()
    {
        DataManager.Instance.PhotoIndex = _userProfileIndex;
        DataManager.Instance.UserName = _userInput.GetComponent<InputField>().text;
        DataManager.Instance.NewId();
        DataManager.Instance.SaveData();

        SceneManager.LoadScene(1);
    }

}
