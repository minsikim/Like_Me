using UnityEngine;
using System;

public class Collectable : MonoBehaviour
{
    //위에서 아래까지 떨어지는 시간
    public float dropToFloorSpeed = 1f;
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
    //All Collectables should
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Drop();
    }

    private void Drop()
    {
        transform.Translate(Vector3.down * _mapHeight / dropToFloorSpeed * Time.deltaTime);
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
                GameManager.AddLikes(1);
                break;
            case CollectableType.Like10:
                GameManager.AddLikes(10);
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
    public float getCurrentSpawnPerSec(DateTime spawnStartTime)
    {
        //최대까지 증가하는 시간: 30초
        float maxGrowthSeconds = 30f;
        float spawnPerSecDelta = spawnPerSecMax - spawnPerSecStart;
        float lifeTime = (DateTime.Now - spawnStartTime).Seconds;
        float growPercentage = (DateTime.Now - spawnStartTime).Seconds / maxGrowthSeconds;

        if (lifeTime < 0) return spawnPerSecStart;

        if (growPercentage > maxGrowthSeconds)
        {
            return spawnPerSecMax;
        }
        else
        {
            return (growPercentage * spawnPerSecDelta) + spawnPerSecStart;
        }
    }
}