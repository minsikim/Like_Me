using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float dropSpeed = 1f;
    public float spawnPerMinute = 1f;
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
        transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);
        if(transform.position.y < Bounds.yMin - 2f)
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
}