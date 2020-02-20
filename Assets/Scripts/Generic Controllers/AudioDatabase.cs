using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioDatabase
{
    static Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    public static AudioClip GetClip(string audioClipId)
    {
        if (audioClips.ContainsKey(audioClipId))
            return audioClips[audioClipId];

        Debug.LogError($"Key '{audioClipId}' was not found in the Audio Database.");
        return audioClips["default"];
    }

    public static void AddClip(SoundClip soundClip)
    {
        audioClips[soundClip.id] = soundClip.audioClip;
    }
}