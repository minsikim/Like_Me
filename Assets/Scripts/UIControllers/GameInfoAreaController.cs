using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoAreaController : MonoBehaviour
{
    public TimerData Timers;

    [ReadOnly]
    public List<GameObject> TimerObjectList;
    [SerializeField]
    private GameObject _gameInfoContainer;

    private const float _initialPosition = -80f;
    private const float _distance = -112f;

    void Start()
    {
        
    }

    void Update()
    {

    }

    public void AddTimer(TimerType timer)
    {
        AddTimerObject(Timers.GetPrefab(timer));
    }
    public void AddTimerObject(GameObject prefab)
    {
        if (prefab == null)
        {
            Debug.LogError("No Assigned Prefab");
            return;
        }
        if (isActiveTimer(prefab.GetComponent<TimerUIController>().type))
        {
            ResetTimer(prefab.GetComponent<TimerUIController>().type);
            return;
        }

        GameObject newTimer = Instantiate(prefab, transform);
        newTimer.transform.SetParent(_gameInfoContainer.transform);
        TimerObjectList.Add(newTimer);

        switch (prefab.GetComponent<TimerUIController>().type)
        {
            case TimerType.Disorienting:
                newTimer.GetComponent<TimerUIController>().OnTimeUp.AddListener(Player.current.Undisorient);
                break;
            case TimerType.Double:
                newTimer.GetComponent<TimerUIController>().OnTimeUp.AddListener(GameSceneManager.current.UnsetDouble);
                break;
            case TimerType.Triple:
                newTimer.GetComponent<TimerUIController>().OnTimeUp.AddListener(GameSceneManager.current.UnsetTriple);
                break;
            default:
                break;
        }

        UpdatePositions();
    }
    public void ResetTimer(TimerType time)
    {
        foreach (GameObject t in TimerObjectList)
        {
            if (t.GetComponent<TimerUIController>().type == time)
                t.GetComponent<TimerUIController>().ResetTime();
        }
    }
    public void DeleteTimerObject(int index)
    {
        GameObject g = TimerObjectList[index];
        TimerObjectList.RemoveAt(index);
        Destroy(g);
        UpdatePositions();
    }
    public void DeleteTimerObject(GameObject timer)
    {
        TimerObjectList.Remove(timer);
        Destroy(timer);
        UpdatePositions();
    }
    public void UpdatePositions()
    {
        for(int i = 0; i < TimerObjectList.Count; i++)
        {
            TimerObjectList[i].transform.localPosition = new Vector3(_initialPosition + (_distance * i) + 450f, 0, 0);
            string name = TimerObjectList[i].GetComponent<TimerUIController>().type.ToString();
            Debug.Log(name + " Position Updated to: " + new Vector3(_initialPosition + (_distance * i) + 450f, 0, 0));
        }
    }
    public bool isActiveTimer(TimerType type)
    {
        foreach(GameObject g in TimerObjectList)
        {
            if (g.GetComponent<TimerUIController>().type == type) return true;
        }
        return false;
    }
}
