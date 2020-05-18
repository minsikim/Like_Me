using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BeforeGamePopupController : MonoBehaviour
{

    public Text currentLevelText;
    public Text targetLevelText;

    public Image LevelProgression;
    public Text LevelProgressionText;

    public Image primaryCollectableImage;
    public Image secondaryCollectableImage;

    public Text primaryCollectableText;
    public Text secondaryCollectableText;

    public StageData stageData;
    public CollectableData collectableData;

    private Level _currentLevel;
    private Level _targetLevel;
    private float _currentLevelProgression;


    void Start()
    {
        _currentLevel = DataManager.Instance.CurrentLevel;
        _targetLevel = DataManager.Instance.CurrentLevel + 1; 

        GetComponent<CanvasGroup>().alpha = 0;

        UpdateCurrentProgression();
    }

    void Update()
    {
        if(GetComponent<CanvasGroup>().alpha < 1)
            GetComponent<CanvasGroup>().alpha += Time.deltaTime * 2f;
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

    public void ToFeedScene()
    {
        SceneManager.LoadScene("PlayerFeed");
    }

    private void UpdateCurrentProgression()
    {
        int _targetLevelFollower = stageData.GetStageData(_targetLevel).FollowerCondition;
        int _currentFollower = DataManager.Instance.Followers;

        currentLevelText.text = stageData.GetStageData(_currentLevel).StageName;
        targetLevelText.text = stageData.GetStageData(_targetLevel).StageName;

        LevelProgression.fillAmount = (float)_currentFollower / _targetLevelFollower;
        LevelProgressionText.text = _currentFollower + " / " + _targetLevelFollower + " Followers";

        CollectableType _primaryCollectableType = stageData.GetStageData(_currentLevel).PrimaryCollectable;
        CollectableType _secondaryCollectableType = stageData.GetStageData(_currentLevel).SecondaryCollectable;

        primaryCollectableImage.sprite = collectableData.GetPrefab(_primaryCollectableType).GetComponent<SpriteRenderer>().sprite;
        secondaryCollectableImage.sprite = collectableData.GetPrefab(_secondaryCollectableType).GetComponent<SpriteRenderer>().sprite;

        primaryCollectableText.text = "+ " + _primaryCollectableType.ToString();
        secondaryCollectableText.text = "+ " +_secondaryCollectableType.ToString();
    }

}
