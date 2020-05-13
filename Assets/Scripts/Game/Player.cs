using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public static Player current;

    public float _direction = 0f;

    public float _speed = 1f;

    public bool _isDead = true;

    public bool _isDisoriented = false;

    public GameObject _image;

    public GameObject _collectedText;

    private int _moving;

    void Awake()
    {
        current = this;
        gameObject.SetActive(false);
    }
    void Start()
    {
        transform.position = new Vector3(0, Bounds.yMin, 0);
        _moving = Animator.StringToHash("Moving");
    }

    void Update()
    {
        CalculateMovement();
        //!TODO: Change to a non-updating function
        ControlAnimation();
    }

    private void ControlAnimation()
    {
        if (_direction == 0f)
        {
            if (_image.GetComponent<Animator>().GetBool(_moving) == false) return;
            else _image.GetComponent<Animator>().SetBool(_moving, false);
        }
        else
        {
            if (_image.GetComponent<Animator>().GetBool(_moving) == true) return;
            else _image.GetComponent<Animator>().SetBool(_moving, true);
            //Flip if _direction < 0
            if(_direction < 0)
            {
                _image.transform.localPosition = new Vector3(1.5f, 0, 0);
                _image.GetComponent<SpriteRenderer>().flipX = true;
            }
            else if(_direction > 0)
            {
                _image.transform.localPosition = new Vector3(0, 0, 0);
                _image.GetComponent<SpriteRenderer>().flipX = false;
            }
            
        }
    }

    private void CalculateMovement()
    {

        float currentDirection = 0;

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _direction = 1f;
        }else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _direction = -1f;
        }
        else
        {
            _direction = 0f;
        }
#endif

        //좌우전환 계산
        if (!_isDisoriented)
        {
            currentDirection = _direction;
        }
        else
        {
            currentDirection = -_direction;
        }

        //움직임
        transform.Translate(new Vector3(currentDirection, 0, 0) * _speed * Time.deltaTime);

        //영역제한
        if (transform.position.x <= Bounds.xMin)
        {
            transform.position = new Vector3(Bounds.xMin, transform.position.y, 0);
        }
        else if (transform.position.x >= Bounds.xMax)
        {
            transform.position = new Vector3(Bounds.xMax, transform.position.y, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collectable")
        {
            other.GetComponent<Collectable>().Collided(this);
        }
        else
        {
            //When Collided with Something Else than Collectables
        }
    }

    public void Disorient()
    {
        //StartCoroutine(DisorientRoutine());
        if (_isDisoriented)
        {
            GameSceneManager.current.ResetTimer(TimerType.Disorienting);
            Debug.Log("Disorient timer reset");
        }
        else
        {
            _isDisoriented = true;
            GameSceneManager.current.AddTimer(TimerType.Disorienting);
            Debug.Log("Player is Disoriented");
        }
    }
    public void Undisorient()
    {
        _isDisoriented = false;
        Debug.LogError("Undisorient");
    }
    public void SpeedUp()
    {
        StartCoroutine(SpeedUpRoutine());
        Debug.Log("Player SpeedUp");
    }
    public void OnPlayerDeath()
    {
        _direction = 0f;
        _isDead = true;
        GameObject _spawnManager = GameObject.Find("Spawn_Manager");
        GameSceneManager _gameManager = GameObject.Find("Game_Scene_Manager").GetComponent<GameSceneManager>();

        if (_spawnManager != null && _gameManager != null)
        {
            _spawnManager.GetComponent<SpawnManager>().StopSpawning();
            _gameManager.OnGameOver();
        }
        else
        {
            Debug.Log("Player.cs: Can not find Spawn Manager or UIManager");
        }
        this.gameObject.SetActive(false);
        Debug.Log("Player is Dead");
    }

    public void setDirection(float directionX)
    {
        //오른쪽 + 왼쪽 - 제자리 0
        _direction = directionX;
    }

    public void SpawnOverHeadText(int amount)
    {
        if (_collectedText != null)
        {
            Vector3 _position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up);
            GameObject c = Instantiate(_collectedText, _position + Vector3.up, Quaternion.identity);
            c.GetComponent<CollectedText>().SetText(amount);

            if (GameObject.Find("InGame_Canvas") != null)
            {
                GameObject p = GameObject.Find("InGame_Canvas");
                c.transform.SetParent(p.transform);
            }

        }
    }

    IEnumerator DisorientRoutine()
    {
        _isDisoriented = true;
        GameSceneManager.current.AddTimer(TimerType.Disorienting);
        yield return new WaitForSeconds(5f);
        _isDisoriented = false;
    }
    IEnumerator SpeedUpRoutine()
    {
        _speed = _speed * 2f;
        yield return new WaitForSeconds(5f);
        _speed = _speed * 0.5f;
    }
}
