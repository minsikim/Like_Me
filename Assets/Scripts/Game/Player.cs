using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public float _direction = 0f;

    public float _speed = 1f;

    public bool _isDead = true;

    public bool _isDisoriented = false;

    void Awake()
    {
        gameObject.SetActive(false);
    }
    void Start()
    {
        transform.position = new Vector3(0, Bounds.yMin, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
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
        StartCoroutine(DisorientRoutine());
        Debug.Log("Player is Disoriented");
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

    IEnumerator DisorientRoutine()
    {
        _isDisoriented = true;
        yield return new WaitForSeconds(5f);
        _isDisoriented = false;
        Debug.Log("Player is Normal");
    }
    IEnumerator SpeedUpRoutine()
    {
        _speed = _speed * 2f;
        yield return new WaitForSeconds(5f);
        _speed = _speed * 0.5f;
        Debug.Log("Player is Normal");
    }
}
