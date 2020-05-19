using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class Collectable : MonoBehaviour
{
    //위에서 아래까지 떨어지는 시간
    public float dropToFloorSpeed = 1f;
    public float gravity = 1f;
    public float spawnPerSecStart = 1f;
    public float spawnPerSecMax = 2f;
    public GameObject particleEffect;

    private float _mapHeight = 8f;

    private AudioChannelController _sfxController;

    public CollectableType collectableType;

    private void Awake()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = gravity;
        _sfxController = AudioManager.Instance.SFXController;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckBoundsAndDestroy();
    }

    private void Drop()
    {
        transform.Translate(Vector3.down * _mapHeight / dropToFloorSpeed * Time.deltaTime);
    }
    private void CheckBoundsAndDestroy()
    {
        if (transform.position.y < Bounds.yMin - 2f)
        {
            Destroy(gameObject);
        }
    }
    public void Collided(Player _player)
    {
        switch (collectableType)
        {
            case CollectableType.Like1:
                GameSceneManager.current.AddLikes(1);
                break;
            case CollectableType.Like2:
                GameSceneManager.current.AddLikes(2);
                break;
            case CollectableType.Like10:
                GameSceneManager.current.AddLikes(10 + UnityEngine.Random.Range(-2, 2 + 1));
                break;
            case CollectableType.Like20:
                GameSceneManager.current.AddLikes(20 + UnityEngine.Random.Range(-4, 4 + 1));
                break;
            case CollectableType.Like50:
                GameSceneManager.current.AddLikes(50 + UnityEngine.Random.Range(-10, 10 + 1));
                break;
            case CollectableType.Like100:
                GameSceneManager.current.AddLikes(100 + UnityEngine.Random.Range(-20, 20 + 1));
                break;
            case CollectableType.Like250:
                GameSceneManager.current.AddLikes(250 + UnityEngine.Random.Range(-50, 50 + 1));
                break;
            case CollectableType.Like500:
                GameSceneManager.current.AddLikes(500 + UnityEngine.Random.Range(-100, 100 + 1));
                break;
            case CollectableType.Like1000:
                GameSceneManager.current.AddLikes(1000 + UnityEngine.Random.Range(-200, 200 + 1));
                break;
            case CollectableType.Like2500:
                GameSceneManager.current.AddLikes(2500 + UnityEngine.Random.Range(-500, 500 + 1));
                break;
            case CollectableType.Like5000:
                GameSceneManager.current.AddLikes(5000 + UnityEngine.Random.Range(-1000, 1000 + 1));
                break;
            case CollectableType.Like10000:
                GameSceneManager.current.AddLikes(10000 + UnityEngine.Random.Range(-2000, 2000 + 1));
                break;
            case CollectableType.Like25000:
                GameSceneManager.current.AddLikes(25000 + UnityEngine.Random.Range(-5000, 5000 + 1));
                break;
            case CollectableType.Like50000:
                GameSceneManager.current.AddLikes(50000 + UnityEngine.Random.Range(-10000, 10000 + 1));
                break;
            case CollectableType.Like100000:
                GameSceneManager.current.AddLikes(100000 + UnityEngine.Random.Range(-20000, 20000 + 1));
                break;
            case CollectableType.Like250000:
                GameSceneManager.current.AddLikes(250000 + UnityEngine.Random.Range(-50000, 50000 + 1));
                break;
            case CollectableType.Like1000000:
                GameSceneManager.current.AddLikes(1000000 + UnityEngine.Random.Range(-200000, 200000 + 1));
                break;
            case CollectableType.Double:
                GameSceneManager.current._double = true;
                GameSceneManager.current.AddTimer(TimerType.Double);
                break;
            case CollectableType.Triple:
                GameSceneManager.current._triple = true;
                GameSceneManager.current.AddTimer(TimerType.Triple);
                break;
            case CollectableType.Disorienting:
                _player.Disorient();
                break;
            case CollectableType.Death:
#if UNITY_EDITOR
                _player.OnPlayerDeath();
                Debug.Log("Died in Editor");
#else
                _player.OnPlayerDeath();
#endif
                break;
            default:
                break;
        }

        if(collectableType == CollectableType.Death)
        {
            _sfxController.PlayOneShot(_sfxController.Audios[3]);
        }
        else if (collectableType == CollectableType.Disorienting)
        {
            _sfxController.PlayOneShot(_sfxController.Audios[2]);
        }
        else
        {
            _sfxController.PlayOneShot(_sfxController.Audios[1]);
        }

        if(particleEffect != null)
        {
            Instantiate(particleEffect, transform.position + (Vector3.up * (Bounds.xMax - Bounds.xMin) * 0.05f), transform.rotation);
        }

        Destroy(gameObject);
    }

    //float maxGrowthSeconds는 스폰이 최대화되는데 까지가는 시간
    public float getCurrentSpawnPerSec(DateTime spawnStartTime, float maxGrowthSeconds)
    {
        DateTime currentTime = DateTime.Now;
        //최대까지 증가하는 시간: 30초
        float spawnPerSecDelta = spawnPerSecMax - spawnPerSecStart;
        float lifeTime = (float)(currentTime - spawnStartTime).TotalSeconds;
        float growPercentage = (float)(currentTime - spawnStartTime).TotalSeconds / maxGrowthSeconds;

        if (lifeTime < 0) return spawnPerSecStart;

        if (growPercentage > 1)
        {
            return spawnPerSecMax;
        }
        else
        {
            return (growPercentage * spawnPerSecDelta) + spawnPerSecStart;
        }
    }
}