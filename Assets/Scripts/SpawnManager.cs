using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //Current Player
    [SerializeField]
    private GameObject _player;

    //Settings
    [SerializeField]
    private float _maxGrowthSeconds = 30f;

    //Stage Information
    [SerializeField]
    private StageData _stageData;
    [SerializeField]
    private CollectableData _collectables;

    //GameObjects in Scene
    [SerializeField]
    private GameObject _collectableContainer;

    //Needed Things to Spawn
    private StageSetting _stageSetting;
    private List<GameObject> _spawnPrefabList = new List<GameObject>();

    //DELETE! Temp to see whether it is assigned correctly
    public GameObject _primaryCollectable;
    public GameObject _secondaryCollectable;

    private DateTime _startSpawnTime;

    private bool _stopSpawning = false;
    void Awake()
    {

    }
    void Start()
    {
        //Get Stage Settings
        _stageSetting = _stageData.GetStageData(DataManager.Instance.CurrentLevel);

        //With the Stage Settings > Get Prefabs this Spawner has to spawn
        _primaryCollectable = _collectables.GetPrefab(_stageSetting.PrimaryCollectable);
        _secondaryCollectable = _collectables.GetPrefab(_stageSetting.SecondaryCollectable);

        //Before Primary Collectable to SpawnList delete Particled Collectables from Primary Collecatable
        _primaryCollectable.GetComponent<Collectable>().particleEffect = null;
        //And Override spawnPerSec Datas
        _primaryCollectable.GetComponent<Collectable>().spawnPerSecStart = 0.9f;
        _primaryCollectable.GetComponent<Collectable>().spawnPerSecMax = 8f;
        _secondaryCollectable.GetComponent<Collectable>().spawnPerSecStart = 0.15f;
        _secondaryCollectable.GetComponent<Collectable>().spawnPerSecMax = 0.6f;
        _secondaryCollectable.transform.localScale *= 1.5f;

        //Add collectables to Spawn List
        _spawnPrefabList.Add(_primaryCollectable);
        _spawnPrefabList.Add(_secondaryCollectable);
        _spawnPrefabList.Add(_collectables.GetPrefab(CollectableType.Death));
        _spawnPrefabList.Add(_collectables.GetPrefab(CollectableType.Disorienting));
        _spawnPrefabList.Add(_collectables.GetPrefab(CollectableType.Double));
        _spawnPrefabList.Add(_collectables.GetPrefab(CollectableType.Triple));
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
        if (_spawnPrefabList != null)
        {
            foreach (GameObject p in _spawnPrefabList)
            {
                StartCoroutine(SpawnCollectableRoutine(p));
            }
        }
        else
        {
            Debug.LogError(" _collectablePrefabs is NULL");
        }
    }
}
