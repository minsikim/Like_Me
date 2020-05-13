using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioVariableGroup", menuName = "Utils/Variables/Audios/AudioVariableGroup", order = 1)]
public class AudioVariableGroup : ScriptableObject 
{
	public List<AudioVariable> Audios = new List<AudioVariable>();
}
