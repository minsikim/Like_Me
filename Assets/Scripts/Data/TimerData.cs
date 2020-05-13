using System;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "TimerData", menuName = "Project L/TimerData")]
public class TimerData : ScriptableObject
{
    public List<TimerSet> TimerSets;

    public GameObject GetPrefab(TimerType type)
    {
        foreach(TimerSet t in TimerSets)
        {
            if (type == t.type)
                return t.prefab;
        }

        return null;
    }
}
[Serializable]
public struct TimerSet
{
    public TimerType type;
    public GameObject prefab;
}