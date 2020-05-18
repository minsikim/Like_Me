using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AfterGamePopupController : MonoBehaviour
{
    public GameObject bestLikeArea;
    public Text bestLikeText;
    public Text currentLikeText;

    public Text currentLevelText;
    public Text targetLevelText;

    public Image LevelProgression;
    public Text LevelProgressionText;

    public GameObject bestTagOnBest;
    public GameObject bestTagOnCurrent;

    public Text levelUpText;
    public Text addedFollowerText;

    public StageData stageData;
    public CollectableData collectableData;

    private Level _currentLevel;
    private Level _targetLevel;
    private float _currentLevelProgression;

    private PostData _currentPostData;

    void Start()
    {
        _currentLevel = DataManager.Instance.CurrentLevel;
        _targetLevel = DataManager.Instance.CurrentLevel + 1;

        _currentPostData = DataManager.Instance.LastPostData;

        GetComponent<CanvasGroup>().alpha = 0;
        UpdateInfo();
    }

    void Update()
    {
        if (GetComponent<CanvasGroup>().alpha < 1)
            GetComponent<CanvasGroup>().alpha += Time.deltaTime;
    }
    public void Confirm()
    {
        PopupCanvasManager.Instance.ClosePopup(gameObject);
        GameInstance.Instance.StartGame();
    }

    public void Close()
    {
        SceneManager.LoadScene("PlayerFeed");
        PopupCanvasManager.Instance.ClosePopup(gameObject);
    }

    public void UpdateInfo()
    {
        //Panel1 update
        if(DataManager.Instance.PostDatas.Count == 1)
        {
            bestLikeArea.SetActive(false);
        }
        else
        {
            bestLikeArea.SetActive(true);

            int _bestLikes = DataManager.Instance.GetBestLikes();
            if (_bestLikes != 0)
                bestLikeText.text = "" + _bestLikes;
            else
                bestLikeText.text = "첫 게시물!";

            if (CheckBetter(_bestLikes, _currentPostData.Likes))
            {
                bestTagOnBest.SetActive(false);
                bestTagOnCurrent.SetActive(true);
                bestLikeText.text = "최고기록!";
            }
            else
            {
                bestTagOnBest.SetActive(true);
                bestTagOnCurrent.SetActive(false);
            }
        }

        currentLikeText.text = "" + _currentPostData.Likes;

        //Panel2 update
        if (CheckLevelUp())
        {
            levelUpText.text = "이제 당신은 " + stageData.GetStageData(_currentLevel).StageName +"입니다";
        }
        else
        {
            levelUpText.text = "당신은 아직 " + stageData.GetStageData(_currentLevel).StageName + "입니다";
        }
        addedFollowerText.text = "팔로워  + " + DataManager.Instance.CalcAddedFollowers(_currentPostData.Likes);

        //Panel3 update
        int _targetLevelFollower = stageData.GetStageData(_targetLevel).FollowerCondition;
        int _currentFollower = DataManager.Instance.Followers;

        currentLevelText.text = stageData.GetStageData(_currentLevel).StageName;
        targetLevelText.text = stageData.GetStageData(_targetLevel).StageName;

        LevelProgression.fillAmount = (float)_currentFollower / _targetLevelFollower;
        LevelProgressionText.text = _currentFollower + " / " + _targetLevelFollower + " Followers";

    }

    public bool CheckBetter(int best, int current)
    {
        Debug.Log("checkBetter b/c: " + best + "/" + current);

        if (best <= current)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckLevelUp()
    {
        StageSetting _previousLevelData = stageData.GetStageData(_currentLevel);
        int _prevFollowers = DataManager.Instance.Followers - DataManager.Instance.CalcAddedFollowers(_currentPostData.Likes);
        int _currFollowers = DataManager.Instance.Followers;

        Debug.Log("_prevFollowers: " + _prevFollowers);
        Debug.Log("_currFollowers: " + _currFollowers);
        Debug.Log("_previousLevelData.FollowerCondition: " + _previousLevelData.FollowerCondition);
        

        if (_prevFollowers < _previousLevelData.FollowerCondition && _currFollowers > _previousLevelData.FollowerCondition)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
