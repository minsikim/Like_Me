using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioMixerControl", menuName = "Utils/AudioMixerControl", order = 1)]
public class AudioMixerControl : ScriptableObject 
{
	public AudioMixer MasterMixer;

	public void SetChannelVolume(string channelName, float volume)
	{
		MasterMixer.SetFloat(channelName, volume);
	}
	public float GetChannelVolume(string channelName)
	{
		float result = 0;
		MasterMixer.GetFloat(channelName, out result);
		return result;
	}
}
