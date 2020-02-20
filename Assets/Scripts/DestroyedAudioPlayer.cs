using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created and controlled by DestroyAudio.
[RequireComponent(typeof(AudioSource))]
public class DestroyedAudioPlayer : MonoBehaviour
{
    public void Init(string audioClipId, float lifeTime, float volume)
    {
        GetComponent<AudioSource>().PlayOneShot(AudioDatabase.GetClip(audioClipId), volume);
        StartCoroutine(Destroy(lifeTime));
    }

    IEnumerator Destroy(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }
}
