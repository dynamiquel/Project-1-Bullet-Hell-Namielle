using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    public List<AudioClip> musicClips = new List<AudioClip>();

    AudioSource musicAudioSource;

    private void Awake()
    {
        musicAudioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(int index, bool replace = true)
    {
        if (!musicAudioSource.isPlaying || (musicAudioSource.isPlaying && replace))
        {
            if (index < musicClips.Count)
            {
                musicAudioSource.clip = musicClips[index];
                musicAudioSource.Play();
            }
        }
    }

    public void Stop()
    {
        if (musicAudioSource.isPlaying)
            musicAudioSource.Stop();
    }
}
