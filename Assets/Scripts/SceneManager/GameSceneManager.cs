using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameSceneManager : MonoBehaviour
{
    public static GameSceneManager current;

    public static int Likes;

    private bool _playing = false;
    public bool _double = false;
    public bool _triple = false;

    public ProfileData profileData;
    public ImageListData postImages;

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
    private GameObject _gameInfoContainer;

    [SerializeField]
    private GameObject _background;

    [SerializeField]
    private Text _userNameText;
    [SerializeField]
    private GameObject _userProfileImage;

    [SerializeField]
    private Text _follwerText;
    [SerializeField]
    private Text _gameoverLikeText;
    [SerializeField]
    private Text _gameDurationText;

    private DateTime _startTime;

    private AudioChannelController _bgmContoller;

    public Level currentLevel;

    private void Awake()
    {
        current = this;
    }
    void Start()
    {

        currentLevel        = DataManager.Instance.CurrentLevel;
        _userNameText.text  = DataManager.Instance.UserName;
        int profileIndex    = DataManager.Instance.PhotoIndex;

        _userProfileImage.GetComponent<Image>().sprite = profileData.ProfileImages[profileIndex].GetComponent<Image>().sprite;

        _bgmContoller = AudioManager.Instance.MusicController;

        if (!_bgmContoller.IsPlaying(_bgmContoller.Audios[0]))
        {
            _bgmContoller.StopAll();
            _bgmContoller.Play(_bgmContoller.Audios[0]);
        }

        GameoverUIDisable();

        GameInstance.Instance.onStartGame += StartGame;

        PopupCanvasManager.Instance.OpenPopup(PopupCanvasManager.Instance.BeforeGamePopupPrefab);
    }

    void Update()
    {
        if (_playing)
        {
            _gameDurationText.text = (int)(DateTime.Now - _startTime).TotalSeconds + " sec";
        }
        
    }

    private void OnDestroy()
    {
        GameInstance.Instance.onStartGame -= StartGame;
    }

    public void StartGame()
    {
        _background.transform.GetChild(0).GetComponent<Animator>().SetBool("Playing", true);

        if (!player.GetComponent<Player>()._isDead)
        {
            Debug.LogError("Can not start player is still alive");
        };
        _controls.SetActive(true);
        GameoverUIDisable();
        //Disable Tutorial Canvas
        _tutorialCanvas.SetActive(false);
        Likes = 0;
        _startTime = DateTime.Now;
        _gameDurationText.text = "0 sec";
        //Enable SpawnManager
        player.SetActive(true);
        player.GetComponent<Player>()._isDead = false;
        Player.current._isDisoriented = false;
        //Enable Player
        _spawnManager.SetActive(true);
        _spawnManager.GetComponent<SpawnManager>().CallSpawn();

        if (!_bgmContoller.IsPlaying(_bgmContoller.Audios[1]))
        {
            _bgmContoller.StopAll();
            _bgmContoller.Play(_bgmContoller.Audios[1]);
        }

        _playing = true;
    }
    public void OnGameOver()
    {
        //Set Post Datas;
        int addedFollowers = DataManager.Instance.AddFollowers(Likes);
        SavePost();

        //Set popup w/ above Data;
        PopupCanvasManager.Instance.OpenPopup(PopupCanvasManager.Instance.AfterGamePopupPrefab);

        _background.transform.GetChild(0).GetComponent<Animator>().SetBool("Playing", false);

        _controls.SetActive(false);
        
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

        if (!_bgmContoller.IsPlaying(_bgmContoller.Audios[0]))
        {
            _bgmContoller.StopAll();
            _bgmContoller.Play(_bgmContoller.Audios[0]);
        }

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
        if(_triple)
        {
            amount = amount * 3;
        }else if (_double)
        {
            amount = amount * 2;
        }
        //Update Game Data: Likes
        Likes += amount;
        //Spawn a collected text above player
        player.GetComponent<Player>().SpawnOverHeadText(amount);
    }

    public void AddTimer(TimerType type)
    {
        _gameInfoContainer.GetComponent<GameInfoAreaController>().AddTimer(type);
        Debug.Log("GameSceneManger: Added Timer :" + type.ToString());
    }
    public void ResetTimer(TimerType type)
    {
        _gameInfoContainer.GetComponent<GameInfoAreaController>().ResetTimer(type);
    }
    public void ToFeedScene()
    {
        SceneManager.LoadScene("PlayerFeed");
    }
    public void ToGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    public void UnsetDouble()
    {
        _double = false;
    }
    public void UnsetTriple()
    {
        _triple = false;
    }
    public void SavePost()
    {
        PostData pD = new PostData
        {
            id = Guid.NewGuid().ToString(),
            SpriteIndex = UnityEngine.Random.Range(0, postImages.Images.Count),
            Likes = Likes,
            PostTime = DateTime.Now.ToString("MM/dd/yyyy h:mm tt"),
            PlayTimeSeconds = (int)(DateTime.Now - _startTime).TotalSeconds
        };
        DataManager.Instance.LastPostData = pD;
        DataManager.Instance.PostDatas.Add(pD);
        DataManager.Instance.SaveData();
    }
}
