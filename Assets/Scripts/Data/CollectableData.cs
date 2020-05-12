﻿using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CollectableData", menuName = "Project L/CollectableData")]
public class CollectableData : ScriptableObject
{
    public List<CollectableSet> Collectables;

    public GameObject GetPrefab(CollectableType type)
    {
        GameObject prefab = new GameObject();

        foreach(CollectableSet s in Collectables)
        {
            if (s.type == type)
                prefab = s.prefab;
        }

        return prefab;
    }
}

[Serializable]
public struct CollectableSet
{
    public CollectableType type;
    public GameObject prefab;
}