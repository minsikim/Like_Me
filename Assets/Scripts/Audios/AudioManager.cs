using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;

public class AudioManager : MonoBehaviour 
{
    private static AudioManager _instance;

    public static AudioManager Instance { get { return _instance; } }

    [SerializeField]
	private AudioSettings _settings;

	[SerializeField]
	private AudioChannelController _musicController;
    public AudioChannelController MusicController { get { return _musicController; } }

	[SerializeField]
	private AudioChannelController _sfxController;
    public AudioChannelController SFXController { get { return _sfxController; } }

	[SerializeField]
	private AudioMixerControl _mixerControl;
	public AudioMixerControl MixerControl{get{return _mixerControl;}}

	public static readonly string MusicChannelName = "Music";
	public static readonly string SFXChannelName = "SFX";

	public static readonly string MusicMasterChannelName = "MusicMaster";
	public static readonly string SFXmasterChannelName = "SFXMaster";

    private IEnumerator _musicRoutine;
    private IEnumerator _sfxRoutine;

	private void Awake()
	{
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
	}

	public void SetMusicVolume(float target, float duration, SetVolumeEndDelegate endDelegate = null)
	{
        if (_musicRoutine != null)
        {
            StopCoroutine(_musicRoutine);
            _musicRoutine = null;
        }
        _musicRoutine = SetChannelVolumeRoutine(MusicChannelName, target, duration, endDelegate);
        StartCoroutine(_musicRoutine);
	}

	public void SetSfxVolume(float target, float duration, SetVolumeEndDelegate endDelegate = null)
	{
        if (_sfxRoutine != null)
        {
            StopCoroutine(_sfxRoutine);
            _sfxRoutine = null;
        }
        _sfxRoutine = SetChannelVolumeRoutine(SFXChannelName, target, duration, endDelegate);
        StartCoroutine(_sfxRoutine);
	}

	public void SetMusicMasterVolume(float target, float duration, SetVolumeEndDelegate endDelegate = null)
	{
		SetChannelVolume(MusicMasterChannelName, target, duration, endDelegate);
	}

	public void SetSfxMasterVolume(float target, float duration, SetVolumeEndDelegate endDelegate = null)
	{
		SetChannelVolume(SFXmasterChannelName, target, duration, endDelegate);
	}

	public void SetChannelVolume(string channelName, float target, float duration, SetVolumeEndDelegate endDelegate = null)
	{
		StartCoroutine(SetChannelVolumeRoutine(channelName, target, duration, endDelegate));
	}

	private IEnumerator SetChannelVolumeRoutine(string channelName, float target, float duration, SetVolumeEndDelegate endDelegate)
	{
		Timer timer = new Timer(duration);
		float start = _mixerControl.GetChannelVolume(channelName);
		while(timer.Tick(Time.deltaTime) == 0)
		{
			_mixerControl.SetChannelVolume(channelName, Mathf.LerpUnclamped(start, target, timer.Percent));		
			yield return null;
		}
		_mixerControl.SetChannelVolume(channelName, target);

		if(endDelegate != null)
		{
			endDelegate.Invoke();
		}
	}

	public delegate void SetVolumeEndDelegate();  
}