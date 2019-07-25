using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    public float _speed = 1.0f;

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

#if UNITY_ANDROID
        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
#else
        float horizontalInput = Input.GetAxis("Horizontal");
#endif
        if (_isDisoriented)
        {
            horizontalInput = -horizontalInput;
        }
        transform.Translate(new Vector3(horizontalInput, 0, 0) * _speed * Time.deltaTime);

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
        _isDead = true;
        GameObject _spawnManager = GameObject.Find("Spawn_Manager");
        UIManager _uiManager = GameObject.Find("UI_Manager").GetComponent<UIManager>();

        if (_spawnManager != null && _uiManager != null)
        {
            _spawnManager.GetComponent<SpawnManager>().StopSpawning();
            _uiManager.GameoverEnable();
        }
        else
        {
            Debug.Log("Player.cs: Can not find Spawn Manager or UIManager");
        }
        this.gameObject.SetActive(false);
        Debug.Log("Player is Dead");
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
        _speed = 2f;
        yield return new WaitForSeconds(5f);
        _speed = 1f;
        Debug.Log("Player is Normal");
    }
}
