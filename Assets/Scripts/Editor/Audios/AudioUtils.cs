using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

using System;
using System.Reflection;

public static class AudioUtils
{
    public static void PlayClip(AudioClip clip)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod("PlayClip", 
                                                      BindingFlags.Static | BindingFlags.Public, 
                                                      null, 
                                                      new System.Type[] { typeof(AudioClip)}, 
                                                      null);
        method.Invoke(null, new object[]{ clip});
    }

    public static void StopClip(AudioClip clip)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod("StopClip", 
                                                      BindingFlags.Static | BindingFlags.Public, 
                                                      null, 
                                                      new System.Type[] { typeof(AudioClip)}, 
                                                      null);
        method.Invoke(null, new object[]{ clip});
    }

    public static void StopAllClip()
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod("StopAllClips", 
                                                      BindingFlags.Static | BindingFlags.Public, 
                                                      null, 
                                                      new System.Type[] { }, 
                                                      null);
        method.Invoke(null, new object[]{ });
    }

    public static bool IsAudioPlaying(AudioClip clip)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod("IsClipPlaying", 
                                                      BindingFlags.Static | BindingFlags.Public, 
                                                      null, 
                                                      new System.Type[] { typeof(AudioClip)}, 
                                                      null);
        bool result = (bool)method.Invoke(null, new object[]{ clip});
        return result;
    }

    public static float GetClipPosition(AudioClip clip)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod("GetClipPosition", 
                                                      BindingFlags.Static | BindingFlags.Public, 
                                                      null, 
                                                      new System.Type[] { typeof(AudioClip)}, 
                                                      null);
        object result = method.Invoke(null, new object[]{ clip});
        return (float)result;
    }

    public static int GetClipSamplePosition(AudioClip clip)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod("GetClipSamplePosition", 
                                                      BindingFlags.Static | BindingFlags.Public, 
                                                      null, 
                                                      new System.Type[] { typeof(AudioClip)}, 
                                                      null);
        object result = method.Invoke(null, new object[]{ clip});
        return (int)result;
    }

    public static void SetClipSamplePosition(AudioClip clip, int iSamplePosition)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod("SetClipSamplePosition", 
                                                      BindingFlags.Static | BindingFlags.Public, 
                                                      null, 
                                                      new System.Type[] { typeof(AudioClip), typeof(int)}, 
                                                      null);
        method.Invoke(null, new object[]{ clip, iSamplePosition});
        return;
    }

    public static int GetSampleCount(AudioClip clip)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod("GetSampleCount", 
                                                      BindingFlags.Static | BindingFlags.Public, 
                                                      null, 
                                                      new System.Type[] { typeof(AudioClip)}, 
                                                      null);
        object result = method.Invoke(null, new object[]{ clip });
        return (int)result;
    }

    public static void UpdateAudio()
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;
        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "UpdateAudio",
            BindingFlags.Static | BindingFlags.Public
            );

        method.Invoke(
            null,
            null
            );
    }

}