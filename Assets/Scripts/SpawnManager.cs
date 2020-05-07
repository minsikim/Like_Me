using System;
using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private float _maxGrowthSeconds = 30f;
    [SerializeField]
    private GameObject _collectableContainer;
    [SerializeField]
    private GameObject[] _collectablePrefabs;

    private DateTime _startSpawnTime;

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
        DateTime tempTime = DateTime.Now;

        while (!_stopSpawning)
        {
            float spawnRate = collectablePrefab.GetComponent<Collectable>().getCurrentSpawnPerSec(_startSpawnTime, _maxGrowthSeconds);
            float randomSpawn = 1 / UnityEngine.Random.Range(spawnRate * 0.75f, spawnRate * 1.25f);
            yield return new WaitForSeconds(randomSpawn);
            if (!_player.GetComponent<Player>()._isDead)
            {
                DateTime spawnTime = DateTime.Now;
                tempTime = spawnTime;

                Vector3 positionSpawn = new Vector3(UnityEngine.Random.Range(Bounds.xMin, Bounds.xMax), Bounds.yMax + UnityEngine.Random.Range(1f, 5f), 0);
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
        _startSpawnTime = DateTime.Now;
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
