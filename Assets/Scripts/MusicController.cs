using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    public List<AudioClip> musicClips = new List<AudioClip>();

    AudioSource musicAudioSource;
    Coroutine currentQueueWait;

    private void Awake()
    {
        musicAudioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Plays an audio clip in the music audio source.
    /// </summary>
    /// <param name="clipIndex">The index of the clip to play</param>
    /// <param name="addState">DontReplace: If a clip is already playing, don't do anything.<br></br>Replace: Always play the clip.<br></br>Queue: If a clip is already playing, play it once the clip has stopped playing.</param>
    /// <param name="maxVolume">The maximum volume the clip will ever reach</param>
    /// <param name="loop">Whether the clip should be looped</param>
    /// <param name="fadeInSpeed">Speed at which to fade-in. 1 = instantaneous</param>
    /// <param name="fadeOutSpeed">Speed at which to fade-out the previous clip (if AddState.Queue). 1 = instantaneous</param>
    public void PlayClip(int clipIndex, AddState addState, float maxVolume = 1f, bool loop = false, float fadeInSpeed = 1f, float fadeOutSpeed = 1f)
    {
        if (clipIndex >= musicClips.Count)
            return;

        if (!musicAudioSource.isPlaying || addState == AddState.Replace)
        {
            musicAudioSource.clip = musicClips[clipIndex];
            StartFadeIn(fadeInSpeed, maxVolume);
            musicAudioSource.loop = loop;

        }
        else if (addState == AddState.Queue)
        {
            if (currentQueueWait != null)
                StopCoroutine(currentQueueWait);

            StartFadeOut(fadeOutSpeed);        
            currentQueueWait = StartCoroutine(WaitForStop(clipIndex, maxVolume, loop, fadeInSpeed));
        }
    }

    // Stops the audio source.
    public void Stop()
    {
        if (musicAudioSource.isPlaying)
            musicAudioSource.clip = null;
    }

    // Adds a fade-in transition to the audio source.
    public void StartFadeIn(float speed, float maxVolume)
    {
        Stop();
        StopAllCoroutines();
        StartCoroutine(FadeIn(speed, maxVolume));
        musicAudioSource.Play();
    }

    // Adds a fade-out transition to the audio source.
    public void StartFadeOut(float speed)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut(speed));
    }

    IEnumerator FadeIn(float speed, float maxVolume)
    {
        float audioVolume = musicAudioSource.volume = 0;

        while (musicAudioSource.volume < maxVolume)
        {
            audioVolume += speed;
            musicAudioSource.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator FadeOut(float speed)
    {
        float audioVolume = musicAudioSource.volume;

        while (musicAudioSource.volume >= speed)
        {
            audioVolume -= speed;
            musicAudioSource.volume = audioVolume;
            yield return new WaitForSeconds(0.1f);
        }

        // Since the loop will end between 0 and speed.
        musicAudioSource.volume = 0f;
        Stop();
    }

    IEnumerator WaitForStop(int clipIndex, float maxVolume, bool loop, float fadeInSpeed)
    {
        yield return new WaitUntil(() => !musicAudioSource.isPlaying);
        PlayClip(clipIndex, AddState.DontReplace, maxVolume, loop, fadeInSpeed);
    }
}

public enum AddState
{
    DontReplace, // If a clip is already playing, don't do anything.
    Replace, // Always play the clip.
    Queue, // If a clip is already playing, play it once the clip has stopped playing.
}