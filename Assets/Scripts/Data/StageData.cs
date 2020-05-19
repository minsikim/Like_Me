using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "StageData", menuName = "Project L/StageData")]
public class StageData : ScriptableObject
{
    public List<StageSetting> StageSettings;
    public StageSetting GetStageData(Level level)
    {
        StageSetting stageSettings = StageSettings[0];

        foreach (StageSetting s in StageSettings)
        {
            if (s.StageLevel == level)
                stageSettings = s;
        }

        return stageSettings;
    }
}

[Serializable]
public struct StageSetting
{
    public Level StageLevel;
    public CollectableType PrimaryCollectable;
    public CollectableType SecondaryCollectable;
    public string StageName;
    public int FollowerCondition;
}