using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ProfileData", menuName = "Project L/StageData")]
public class StageData : ScriptableObject
{
    public List<StageSetting> StageSettings;
}

public struct StageSetting
{
    public Level StageLevel;
    GameObject PrimaryCollectable;
    GameObject SecondaryCollectable;
}