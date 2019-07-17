using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;
    [SerializeField]
    private GameObject _collectableContainer;
    [SerializeField]
    private GameObject[] _collectablePrefabs;

    private bool _stopSpawning = false;
    void Awake()
    {

    }
    void Start()
    {

    }

    void Update()
    {
        
    }

    IEnumerator SpawnCollectableRoutine(GameObject collectablePrefab)
    {
        while (!_stopSpawning)
        {
            float spawnRate = 60 / collectablePrefab.GetComponent<Collectable>().spawnPerMinute;
            float randomSpawn = Random.Range(spawnRate * 0.75f, spawnRate * 1.25f);
            yield return new WaitForSeconds(randomSpawn);
            if (!_player.GetComponent<Player>()._isDead)
            {
                Vector3 positionSpawn = new Vector3(Random.Range(Bounds.xMin, Bounds.xMax), Bounds.yMax + 1f, 0);
                GameObject newCollectable = Instantiate(collectablePrefab, positionSpawn, Quaternion.identity);
                newCollectable.transform.parent = _collectableContainer.transform;
            }
        }

        //NEVER GET HERE CUZ OF INFINITE LOOP
    }

    public void StopSpawning()
    {
        _stopSpawning = true;
    }
    public void CallSpawn()
    {
        _stopSpawning = false;
        if (_collectablePrefabs != null)
        {
            foreach (GameObject p in _collectablePrefabs)
            {
                StartCoroutine(SpawnCollectableRoutine(p));
            }
        }
        else
        {
            Debug.LogError(" _collectablePrefabs is NULL");
        }
        Debug.Log("Start Spawning: " + _stopSpawning);
    }
}
