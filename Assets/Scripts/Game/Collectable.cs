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

    private float _mapHeight = 8f;

    public enum CollectableType // your custom enumeration
    {
        Like,
        Like10,
        Devil,
        Delete,
        Disorienting,
        Death
    };

    public CollectableType collectableType;

    private void Awake()
    {
        gameObject.GetComponent<Rigidbody2D>().gravityScale = gravity;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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
            case CollectableType.Like:
                GameSceneManager.AddLikes(1);
                break;
            case CollectableType.Like10:
                GameSceneManager.AddLikes(10);
                break;
            case CollectableType.Devil:

                break;
            case CollectableType.Delete:

                break;
            case CollectableType.Disorienting:
                _player.Disorient();
                break;
            case CollectableType.Death:
                _player.OnPlayerDeath();
                break;
            default:
                break;
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