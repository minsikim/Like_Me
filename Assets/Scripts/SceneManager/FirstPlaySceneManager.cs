using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlaySceneManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _userInput;
    [SerializeField]
    private GameObject _userProfiles;
    [SerializeField]
    

    private int _userProfileIndex;


    void Start()
    {
        if(DataManager.Instance.UserName != null)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public void SelectProfile(int index)
    //{
    //    Debug.Log(_userProfiles.transform.childCount + " " + index);
    //    for(int i = 0; i < _userProfiles.transform.childCount; i++)
    //    {
    //        if(i != index)
    //        {
    //            Debug.Log(_userProfiles.transform.GetChild(index).gameObject.name);
    //            Debug.Log(_userProfiles.transform.GetChild(index).GetChild(0).gameObject.name);
    //            _userProfiles.transform.GetChild(index).GetChild(0).gameObject.SetActive(true);
    //        }
    //        else
    //        {
    //            Debug.Log(_userProfiles.transform.GetChild(index).gameObject.name);
    //            Debug.Log(_userProfiles.transform.GetChild(index).GetChild(0).gameObject.name);
    //            _userProfiles.transform.GetChild(index).GetChild(0).gameObject.SetActive(false);
    //        }
    //    }

    //}
    public void SelectProfile(int index)
    {
        
        //selectionUI.SetActive(true);
    }

}
