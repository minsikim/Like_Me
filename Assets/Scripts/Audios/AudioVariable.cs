using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioVariable", menuName = "Utils/Variables/Audios/AudioVariable", order = 1)]
public class AudioVariable : ScriptableObject
{
	public AudioClip Clip;
	public float Volume = 1.0f;
}
