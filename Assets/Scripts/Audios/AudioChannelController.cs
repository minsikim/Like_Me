using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;


public class AudioChannelController : MonoBehaviour 
{
	[System.Serializable]
	public struct AudioSourceObjectCreateParam
	{
		public bool ShoudlCreate;
		public AudioVariable Variable;	
	}

	private AudioManager _audioManager;

	public AudioMixerGroup _mixGroup;

	[SerializeField]
	private List<AudioVariable> _audios;
    public List<AudioVariable> Audios { get { return _audios; } }

	[Space]
	[Header("Settings")]
	[SerializeField]
	private bool _defaultLoop = false;
    public bool DefaultLoop { get { return _defaultLoop; } }

    private Dictionary<string, AudioSource> _audioSourceDictionary;
    private Dictionary<string, AudioClip> _audioDictionary;

    private AudioSource _firstAudioSource = null;

	private void Awake()
	{
		_audioManager = GetComponentInParent<AudioManager>();

		_firstAudioSource = new GameObject("FreeAudioSource").AddComponent<AudioSource>();
		_firstAudioSource.transform.SetParent(transform);
		_firstAudioSource.outputAudioMixerGroup = _mixGroup;

		//For Dev
		foreach(var a in Audios)
		{
            if(a == null)
            {
                continue;
            }
			GameObject child = new GameObject(a.name);
			child.transform.SetParent(transform);
			AudioSource audioSource = child.AddComponent<AudioSource>();
			audioSource.outputAudioMixerGroup = _mixGroup;
            audioSource.loop = DefaultLoop;
            audioSource.playOnAwake = false;

            audioSource.clip = a.Clip;
		}

		_audioDictionary = new Dictionary<string, AudioClip>();
		_audioSourceDictionary = new Dictionary<string, AudioSource>();

		for(int i = 0; i < transform.childCount; ++i)
		{
			Transform c = transform.GetChild(i);
			AudioSource audioSource = c.GetComponent<AudioSource>();

			_audioSourceDictionary.Add(c.gameObject.name, audioSource);
			_audioDictionary.Add(c.name, audioSource.clip);
		}
	}

	public void PlayOneShot(AudioVariable variable)
	{
		_firstAudioSource.PlayOneShot(variable.Clip);
	}

	public void Play(string name)
	{
		AudioClip clip = FindAudioVariable(name);
		AudioSource source = FindAudioSource(name);
		if(clip != null && source != null)
		{
			source.clip = clip;
			source.Play();
		}
	}

	public void Play(AudioVariable variable)
	{
		AudioSource source = FindAudioSource(variable.name);
		if(variable.Clip != null && source != null)
		{
			source.clip = variable.Clip;
			source.Play();
		}
	}

	public void PlayOneShot(string name)
	{
		AudioClip clip = FindAudioVariable(name);
		if(clip != null)
		{
			_firstAudioSource.PlayOneShot(clip);
		}
	}

	public void Stop(string name)
	{
		AudioClip clip = FindAudioVariable(name);
		AudioSource source = FindAudioSource(name);
		if(clip != null && source != null)
		{
			source.Stop();
		}
	}

	public void StopAll()
	{
		foreach(var source in _audioSourceDictionary)
		{
			source.Value.Stop();
		}
	}

    public bool IsPlaying(AudioVariable variable)
    {
        return IsPlaying(variable.name);
    }
	public bool IsPlaying(string name)
	{
		AudioSource source = FindAudioSource(name);
		if(source != null)
			return source.isPlaying;
		return false;
	}

	private AudioClip FindAudioVariable(string name)
	{
		AudioClip value = null;
		if(_audioDictionary.TryGetValue(name, out value))
			return value;
		return null;
	}
	private AudioSource FindAudioSource(string name)
	{
		AudioSource source = null;
		if(_audioSourceDictionary.TryGetValue(name, out source))
			return source;
		return null;
	}


}