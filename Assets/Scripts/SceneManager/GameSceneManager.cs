using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Bounds
{
    public static float xMax { get; set; }
    public static float xMin { get; set; }
    public static float yMax { get; set; }
    public static float yMin { get; set; }

}



public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager current;

    public static int Likes;

    private bool _playing = false;

    public ProfileData profileData;

    public GameObject player;
    [SerializeField]
    private GameObject _collectableContainer;
    [SerializeField]
    public GameObject InGameCanvas;
    [SerializeField]
    private GameObject _spawnManager;
    [SerializeField]
    private GameObject _tutorialCanvas;
    [SerializeField]
    private GameObject _gameoverCanvas;
    [SerializeField]
    private GameObject _controls;
    [SerializeField]
    private GameObject _playButton;

    [SerializeField]
    private GameObject _background;

    [SerializeField]
    private Text _userNameText;
    [SerializeField]
    private GameObject _userProfileImage;

    public GameObject CollectedText;

    [SerializeField]
    private Text _follwerText;
    [SerializeField]
    private Text _gameoverLikeText;
    [SerializeField]
    private Text _gameDurationText;

    private DateTime _startTime;

    public Level currentLevel;

    private void Awake()
    {
        current = this;
    }
    void Start()
    {
        Bounds.xMin = -3.8f;
        Bounds.xMax = 3.6f;
        Bounds.yMin = -1.8f;
        Bounds.yMax = 6.3f;

        currentLevel        = DataManager.Instance.CurrentLevel;
        _userNameText.text  = DataManager.Instance.UserName;
        int profileIndex    = DataManager.Instance.PhotoIndex;

        _userProfileImage.GetComponent<Image>().sprite = profileData.ProfileImages[profileIndex].GetComponent<Image>().sprite;

        GameoverUIDisable();
    }

    void Update()
    {
        if (_playing)
        {
            _gameDurationText.text = (int)(DateTime.Now - _startTime).TotalSeconds + " sec";
        }
        
    }

    public void StartGame()
    {
        _background.GetComponent<Animator>().SetBool("Playing", true);

        if (!player.GetComponent<Player>()._isDead)
        {
            Debug.LogError("Can not start player is still alive");
        };
        _controls.SetActive(true);
        _playButton.SetActive(false);
        GameoverUIDisable();
        //Disable Tutorial Canvas
        _tutorialCanvas.SetActive(false);
        Likes = 0;
        _startTime = DateTime.Now;
        _gameDurationText.text = "0 sec";
        //Enable SpawnManager
        player.SetActive(true);
        player.GetComponent<Player>()._isDead = false;
        player.GetComponent<Player>()._isDisoriented = false;
        //Enable Player
        _spawnManager.SetActive(true);
        _spawnManager.GetComponent<SpawnManager>().CallSpawn();
        
        _playing = true;
    }
    public void OnGameOver()
    {
        _background.GetComponent<Animator>().SetBool("Playing", false);

        _controls.SetActive(false);
        _playButton.SetActive(true);
        //Add Followers
        _gameoverLikeText.text = Likes.ToString();
        int addedFollowers = DataManager.Instance.AddFollowers(Likes);
        _follwerText.text = "+" + addedFollowers.ToString();
        //Delete all collectables from container
        foreach (Transform p in _collectableContainer.transform)
        {
            GameObject.Destroy(p.gameObject);
        }
        _tutorialCanvas.SetActive(false);
        _gameoverCanvas.SetActive(true);
        player.SetActive(false);
        _spawnManager.SetActive(false);
        GameoverUIEnable();
        _playing = false;
    }

    public void GameoverUIEnable()
    {
        _gameoverCanvas.SetActive(true);
    }
    public void GameoverUIDisable()
    {
        _gameoverLikeText.text = "0 Likes";
        _gameoverCanvas.SetActive(false);
    }

    public void AddLikes(int amount)
    {
        //Update Game Data: Likes
        Likes += amount;
        //Spawn a collected text above player
        player.GetComponent<Player>().SpawnOverHeadText(amount);
   
    }
    public void ToFeedScene()
    {
        SceneManager.LoadScene("PlayerFeed");
    }
    public void ToGameScene()
    {
        SceneManager.LoadScene("Game");
    }
}
