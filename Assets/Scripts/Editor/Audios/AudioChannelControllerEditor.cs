using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(AudioChannelController))]
public class AudioChannelControllerEditor : Editor
{
    private AudioChannelController _target;

    private void OnEnable()
    {
        _target = target as AudioChannelController;
    }

    private void OnDisable()
    {
        _target = null;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if(!Application.isPlaying)
        {
            if(GUILayout.Button("Create AudioSources"))
            {
                CreateAudioSources();
            }
        }
    }

    private void CreateAudioSources()
    {
        for(int i = _target.transform.childCount - 1; i >= 0; --i)
        {
            DestroyImmediate(_target.transform.GetChild(i).gameObject);
        }

		AudioManager audioManager = Object.FindObjectOfType<AudioManager>();

		int count = 0;
		foreach(var a in _target.Audios)
		{
            if(a == null)
            {
                continue;
            }
			GameObject child = new GameObject(a.name);
			child.transform.SetParent(_target.transform);
			AudioSource audioSource = child.AddComponent<AudioSource>();
			audioSource.outputAudioMixerGroup = _target._mixGroup;
            audioSource.loop = _target.DefaultLoop;
            audioSource.playOnAwake = false;

            audioSource.clip = a.Clip;

			count++;
		}

        EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());

    }
}