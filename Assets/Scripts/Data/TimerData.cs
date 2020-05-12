using System;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "TimerData", menuName = "Project L/TimerData")]
public class TimerData : ScriptableObject
{
    public List<TimerSet> TimerSets;

    public GameObject GetPrefab(TimerType type)
    {
        GameObject prefab = new GameObject();

        foreach(TimerSet t in TimerSets)
        {
            if (type == t.type)
                prefab = t.prefab;
        }

        return prefab;
    }
}
[Serializable]
public struct TimerSet
{
    public TimerType type;
    public GameObject prefab;
}