using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;

using UnityEditor;

using System.IO;

public class SoundEditorWindow : EditorWindow
{
    private const string DefaultAudioPath = "Assets/Resources/Variables/Audios";
    private const string DefaultAudioGroupPath = "Assets/Resources/Variables/AudioGroups";

    private const string DefaultBgmAudioPath = "Assets/Resources/Variables/Audios/Bgms";
    private const string DefaultSystemAudioPath = "Assets/Resources/Variables/Audios/Systems";
    private const string DefaultSfxAudioPath = "Assets/Resources/Variables/Audios/Sfxs";

    private readonly string DefaultAudioFolderPath = "/Scripts/Audios";

    private static Texture2D _playButtonTexture;
    private static Texture2D _stopButtonTexture;

    private static string PlayingAudioName = "";

    private static Vector2 _scrollPosition = Vector2.zero;

    private GUIStyle _titleStyle;
    private GUIStyle _subTitleStyle;

    private AudioVariableGroup _bgmGroup;
    private AudioVariableGroup _sfxGroup;

    private const int HorizontalAudioInfoViewNum = 2;

    private static List<AudioVariable> _bgmVariables = new List<AudioVariable>();
    private static bool _showBgms = true;
    private static List<AudioVariable> _sfxVariables = new List<AudioVariable>();
    private static bool _showSfxs = true;

    private Event _currentEvent;


    [MenuItem("CustomTools/Sounds/Sound Editor Window")]
    public static void ShowWindow()
    {
        var window = GetWindow<SoundEditorWindow>();
        window.titleContent = new GUIContent("Sound Editor Window");

        window.Init();
    }

    private void Init()
    {
        _titleStyle = new GUIStyle(EditorStyles.boldLabel);
        _titleStyle.fontSize = 30;

        _subTitleStyle = new GUIStyle(EditorStyles.boldLabel);
        _subTitleStyle.fontSize = 20;

        _bgmGroup = AssetDatabase.LoadAssetAtPath<AudioVariableGroup>(DefaultAudioGroupPath + "/BgmGroup.asset");
        _sfxGroup = AssetDatabase.LoadAssetAtPath<AudioVariableGroup>(DefaultAudioGroupPath + "/SfxGroup.asset");

        _bgmVariables = EditorUtils.GetAssetsOfType<AudioVariable>(DefaultBgmAudioPath, "t:AudioVariable");
        _sfxVariables = EditorUtils.GetAssetsOfType<AudioVariable>(DefaultSfxAudioPath, "t:AudioVariable");

        _bgmGroup.Audios.Clear();
        _bgmGroup.Audios.AddRange(_bgmVariables.ToArray());
        _sfxGroup.Audios.Clear();
        _sfxGroup.Audios.AddRange(_sfxVariables.ToArray());

        _playButtonTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Hex/Editor/Textures/baseline_play_arrow_black_18dp.png");
        _stopButtonTexture = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/Hex/Editor/Textures/baseline_stop_black_18dp.png");
    }

    //윈도우 꺼질때 사운드도 꺼진다.
    private void OnDestroy()
    {
        StopAll();
        AssetDatabase.SaveAssets();
    }

    private void OnGUI()
    {
        _currentEvent = Event.current;

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

        #region Draw Title
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Sound Editor Window", _titleStyle, GUILayout.Height(45));
            EditorGUILayout.Space();
        }
        #endregion

        {
            if (GUILayout.Button("Stop All Sound"))
                AudioUtils.StopAllClip();
            if(GUILayout.Button("Create AudioStringsFile"))
                CreateAudioStringFile();
        }

        #region Draw Bgms
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("  Bgms", _subTitleStyle, GUILayout.Height(30));
            _showBgms = EditorGUILayout.Foldout(_showBgms, "Fold");
            if (_showBgms)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                DrawAudioVariableGroup(_bgmGroup, DefaultBgmAudioPath, "NewBgm.asset");
                EditorGUILayout.EndVertical();
            }
        }
        #endregion

        #region Draw Sfx
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("  Sfxs", _subTitleStyle, GUILayout.Height(30));
            _showSfxs = EditorGUILayout.Foldout(_showSfxs, "Fold");
            if (_showSfxs)
            {
                EditorGUILayout.BeginVertical(EditorStyles.helpBox);
                DrawAudioVariableGroup(_sfxGroup, DefaultSfxAudioPath, "NewSfx.asset");
                EditorGUILayout.EndVertical();
            }
        }
        #endregion
        EditorGUILayout.EndScrollView();
    }

    private void DrawAudioVariableGroup(AudioVariableGroup variable, string audioPath, string newAssetName)
    {
        EditorGUILayout.Space();
        {
            EditorGUI.BeginChangeCheck();
            for (int i = 0; i < variable.Audios.Count; ++i)
            {
                DrawAudioVariable(variable, variable.Audios[i]);
            }
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(variable);
            }
        }
        EditorGUILayout.Space();

        if (GUILayout.Button("Add new Audio Variable"))
        {
            AudioVariable newAudio = AudioVariable.CreateInstance<AudioVariable>();
            string uniquePath = AssetDatabase.GenerateUniqueAssetPath(audioPath + "/_" + newAssetName);
            AssetDatabase.CreateAsset(newAudio, uniquePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            variable.Audios.Add(newAudio);
        }
        EditorGUILayout.Space();
    }

    private void DrawAudioVariable(AudioVariableGroup group, AudioVariable variable)
    {
        EditorGUILayout.BeginHorizontal();

        string tmpName = EditorGUILayout.DelayedTextField(variable.name);
        if (tmpName != variable.name)
        {
            string path = AssetDatabase.GetAssetPath(variable);
            AssetDatabase.RenameAsset(path, tmpName);
        }

        EditorGUI.BeginChangeCheck();
        variable.Clip = (AudioClip)EditorGUILayout.ObjectField(variable.Clip, typeof(AudioClip), false);
        if (EditorGUI.EndChangeCheck())
        {
            EditorUtility.SetDirty(variable);
        }

        bool audioExist = variable.Clip != null;
        bool isPlaying = IsPlaying(variable.Clip);

        EditorGUILayout.Space();

        EditorGUI.BeginDisabledGroup(!audioExist);
        if (GUILayout.Button(_playButtonTexture, GUILayout.MaxWidth(26), GUILayout.MaxHeight(28)) &&  
            variable.Clip != null)
        {
            Play(variable.Clip);
        }

        EditorGUI.BeginDisabledGroup(!IsPlaying(variable.Clip));
        if (GUILayout.Button(_stopButtonTexture, GUILayout.MaxWidth(26), GUILayout.MaxHeight(28)) && 
            variable.Clip != null)
        {
            StopAll();
        }
        EditorGUI.EndDisabledGroup();
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.Space();

        DrawAudioProgressBar(variable.Clip, isPlaying);

        EditorGUILayout.Space();
        TimeSpan time = TimeSpan.FromSeconds((variable.Clip == null) ? 0.0f : variable.Clip.length);
        EditorGUILayout.LabelField(time.Hours.ToString("D2") + ":" + 
                                   time.Minutes.ToString("D2") + ":" + 
                                   time.Seconds.ToString("D2") + ":" + 
                                   time.Milliseconds.ToString("D2").Substring(0, 2));

        EditorGUILayout.Space();

        if (GUILayout.Button("Remove"))
        {
            group.Audios.Remove(variable);
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(variable));
        }

        EditorGUILayout.EndHorizontal();
    }

    private void Play(AudioClip clip)
    {
        if (IsPlaying(clip))
        {
            StopAll();
        }
        AudioUtils.PlayClip(clip);
        PlayingAudioName = clip.name;
    }

    private void Stop(AudioClip clip)
    {
        AudioUtils.StopClip(clip);
        PlayingAudioName = "";
    }

    private void StopAll()
    {
        AudioUtils.StopAllClip();
    }

    private bool IsPlaying(AudioClip clip)
    {
        if (clip == null)
        {
            return false;
        }
        return AudioUtils.IsAudioPlaying(clip) && clip.name == PlayingAudioName;
    }


    private void DrawAudioProgressBar(AudioClip clip, bool isPlaying)
    { 
        float progress = 0.0f;

        int sampleCount = clip == null ? 0 : clip.samples;
        if (isPlaying)
        {
            int currentPosition = AudioUtils.GetClipSamplePosition(clip);
            progress = (float)currentPosition / (float)sampleCount;
            Repaint();
        }

        Rect progressRect = EditorGUILayout.BeginHorizontal();
        progressRect.y += 7;
        progressRect.height += 4;

        if (_currentEvent.isMouse &&
            _currentEvent.type == EventType.MouseDown &&
            _currentEvent.button == 0 &&
            isPlaying && progressRect.Contains(_currentEvent.mousePosition))
        {
            Vector2 mousePosition = _currentEvent.mousePosition;
            Vector2 localMousePos = Vector2.zero;
            localMousePos.x = mousePosition.x - progressRect.x;
            localMousePos.y = mousePosition.y - progressRect.y;

            float localProgress = localMousePos.x / progressRect.width;

            int targetSample = (int)(sampleCount * localProgress);
            Debug.Log(sampleCount);

            AudioUtils.SetClipSamplePosition(clip, targetSample);
        }

        EditorGUILayout.Space();
        EditorGUI.ProgressBar(progressRect, progress, "");
        EditorGUILayout.Space();
        EditorGUILayout.EndHorizontal();
    }

    public void CreateAudioStringFile()
    {
        string allStrings = "";

        string bgmEnums = "public static class Bgms {" + Environment.NewLine;
        for(int i = 0; i < _bgmGroup.Audios.Count; ++i)
        {
            bgmEnums +=  "public static string " + _bgmGroup.Audios[i].name + " = " + @"""" + _bgmGroup.Audios[i].name + @"""" + ";" +  Environment.NewLine;
        }
        bgmEnums += "};";

        string sfxEnums = "public static class Sfxs {" + Environment.NewLine;
        for(int i = 0; i < _sfxGroup.Audios.Count; ++i)
        {
            sfxEnums +=  "public static string " + _sfxGroup.Audios[i].name + " = " + @"""" + _sfxGroup.Audios[i].name + @""""+  ";" +  Environment.NewLine;

        }
        sfxEnums += "};";

        // string systemEnums = "public static class SystemSfxs {" + Environment.NewLine;
        // for(int i = 0; i < _systemGroup.Audios.Count; ++i)
        // {
        //     systemEnums +=  "public static string " + _systemGroup.Audios[i].name + " = " + @"""" + _systemGroup.Audios[i].name + @"""" +  ";" +  Environment.NewLine;
        // }
        // systemEnums += "};";

        allStrings += bgmEnums;
        allStrings += Environment.NewLine;
        allStrings += sfxEnums;
        allStrings += Environment.NewLine;
        // allStrings += systemEnums;
        // allStrings += Environment.NewLine;

        using(System.IO.StreamWriter file = 
            new System.IO.StreamWriter(@Application.dataPath + DefaultAudioFolderPath + "/AudioStrings.cs", false))
        {
            file.Write(allStrings);
        }

        AssetDatabase.Refresh();
    } 
}